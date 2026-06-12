using System.Collections.Concurrent;
using HeavenlyKingdom.Domain.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HeavenlyKingdom.Api.Controllers
{
    [ApiController]
    [Route("api/candle-payment")]
    public class CandlePaymentController : ControllerBase
    {
        // In-memory sessions — no migration needed, resets on server restart
        private static readonly ConcurrentDictionary<string, CandlePaymentSession> Sessions = new();

        private static readonly Dictionary<string, decimal> Prices = new()
        {
            ["simple"]  = 15,
            ["large"]   = 35,
            ["festive"] = 75,
        };

        // POST /api/candle-payment/start — requires auth, creates a pending session
        [HttpPost("start")]
        [Authorize]
        public IActionResult Start([FromBody] StartCandlePaymentDto dto)
        {
            if (!Prices.ContainsKey(dto.Type))
                return BadRequest(new { message = "Неверный тип свечи" });
            if (dto.Quantity <= 0 || dto.Quantity > 50)
                return BadRequest(new { message = "Неверное количество" });

            var sessionId = Guid.NewGuid().ToString("N")[..16];
            var total     = Prices[dto.Type] * dto.Quantity;

            Sessions[sessionId] = new CandlePaymentSession
            {
                Id          = sessionId,
                CandleType  = dto.Type,
                Quantity    = dto.Quantity,
                TotalAmount = total,
                Status      = "pending",
                CreatedAt   = DateTime.UtcNow,
            };

            return Ok(new { sessionId, totalAmount = total, candleType = dto.Type, quantity = dto.Quantity });
        }

        // GET /api/candle-payment/{sessionId} — public, returns session summary for payment page
        [HttpGet("{sessionId}")]
        public IActionResult Get(string sessionId)
        {
            if (!Sessions.TryGetValue(sessionId, out var s))
                return NotFound(new { message = "Сессия не найдена" });

            return Ok(new
            {
                id          = s.Id,
                candleType  = s.CandleType,
                quantity    = s.Quantity,
                totalAmount = s.TotalAmount,
                status      = s.Status,
            });
        }

        // POST /api/candle-payment/{sessionId}/process — mock payment gateway
        [HttpPost("{sessionId}/process")]
        public IActionResult Process(string sessionId, [FromBody] ProcessCandlePaymentDto dto)
        {
            if (!Sessions.TryGetValue(sessionId, out var session))
                return Ok(new { success = false, message = "Сессия не найдена или истекла. Вернитесь и попробуйте ещё раз." });

            // Allow retrying after a failed attempt; block only already-paid sessions
            if (session.Status == "paid")
                return Ok(new { success = false, message = "Эта сессия уже оплачена." });

            // Reset to pending so retry works after a failed card
            session.Status = "pending";

            // Validate card number (16 digits, spaces/dashes ignored)
            var digits = (dto.CardNumber ?? "").Replace(" ", "").Replace("-", "");
            if (digits.Length != 16 || !digits.All(char.IsDigit))
                return Ok(new { success = false, message = "Неверный номер карты" });

            if (string.IsNullOrWhiteSpace(dto.CardHolder))
                return Ok(new { success = false, message = "Введите имя держателя карты" });

            // Validate expiry MM/YY
            var parts = (dto.Expiry ?? "").Split('/');
            if (parts.Length != 2 || parts[0].Length != 2 || parts[1].Length < 2)
                return Ok(new { success = false, message = "Неверная дата действия карты" });

            if (string.IsNullOrWhiteSpace(dto.Cvv) || dto.Cvv.Length < 3)
                return Ok(new { success = false, message = "Неверный CVV" });

            // Cards starting with 0000 always fail (test case for declined payment)
            if (digits.StartsWith("0000"))
            {
                session.Status = "failed";
                return Ok(new { success = false, message = "Платёж отклонён банком" });
            }

            session.Status = "paid";
            return Ok(new
            {
                success    = true,
                message    = "Оплата прошла успешно",
                candleType = session.CandleType,
                quantity   = session.Quantity,
            });
        }
    }

    public class CandlePaymentSession
    {
        public string   Id          { get; set; } = "";
        public string   CandleType  { get; set; } = "";
        public int      Quantity    { get; set; }
        public decimal  TotalAmount { get; set; }
        public string   Status      { get; set; } = "pending";
        public DateTime CreatedAt   { get; set; }
    }
}
