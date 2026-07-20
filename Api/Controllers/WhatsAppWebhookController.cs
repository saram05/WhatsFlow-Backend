using Microsoft.AspNetCore.Mvc;

namespace WhatsFlow.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WebhookController : ControllerBase
{
    private readonly ILogger<WebhookController> _logger;
    private readonly string _verifyToken;

    public WebhookController(ILogger<WebhookController> logger, IConfiguration configuration)
    {
        _logger = logger;
        _verifyToken = configuration["WhatsApp:VerifyToken"] ?? "whatsflow_verify_token";
    }

    /// <summary>
    /// GET — Verificación del webhook por Meta
    /// </summary>
    [HttpGet("whatsapp")]
    public IActionResult VerifyWebhook(
        [FromQuery(Name = "hub.mode")] string mode,
        [FromQuery(Name = "hub.challenge")] string challenge,
        [FromQuery(Name = "hub.verify_token")] string verifyToken)
    {
        _logger.LogInformation("Webhook verification request received. Mode: {Mode}", mode);

        // Validar que el modo es "subscribe"
        if (mode != "subscribe")
        {
            _logger.LogWarning("Invalid mode: {Mode}", mode);
            return Unauthorized(new { error = "Invalid mode" });
        }

        // Validar el token de verificación
        if (verifyToken != _verifyToken)
        {
            _logger.LogWarning("Invalid verify token provided");
            return Unauthorized(new { error = "Invalid verify token" });
        }

        // Retornar el challenge para confirmar que el webhook es válido
        _logger.LogInformation("Webhook verified successfully");
        return Ok(challenge);
    }

    /// <summary>
    /// POST — Recibir mensajes entrantes de WhatsApp
    /// </summary>
    [HttpPost("whatsapp")]
    public async Task<IActionResult> ReceiveMessage([FromBody] object payload)
    {
        try
        {
            _logger.LogInformation("Received WhatsApp webhook payload: {@Payload}", payload);

            // Aquí irá la lógica para procesar los mensajes entrantes
            // Por ahora solo reconocemos la recepción

            return Ok(new { success = true });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing WhatsApp webhook");
            return StatusCode(500, new { error = "Internal server error" });
        }
    }
}
