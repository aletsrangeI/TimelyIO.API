namespace Common.Services;

public static class WhatsAppTemplateBuilder
{
    public static string SanitizarTelefono(string? telefono)
    {
        if (string.IsNullOrWhiteSpace(telefono)) return string.Empty;
        var digits = new string(telefono.Where(char.IsDigit).ToArray());
        
        // Si tiene 10 dígitos (número estándar en México), anteponer código país 52
        if (digits.Length == 10)
        {
            return $"52{digits}";
        }

        return digits;
    }

    public static string MensajeConfirmacionInmediata(
        string cliente,
        string servicio,
        string profesional,
        string negocio,
        DateTime fechaHoraLocal,
        Guid tokenAcceso,
        string baseUrl = "https://timely.io")
    {
        var fechaStr = fechaHoraLocal.ToString("dddd d 'de' MMMM, h:mm tt");
        var link = $"{baseUrl.TrimEnd('/')}/mi-cita/{tokenAcceso}";

        return $"👋 ¡Hola {cliente}! Tu cita para *{servicio}* en *{negocio}* ha sido agendada con éxito.\n\n" +
               $"🗓 *Fecha y Hora:* {fechaStr}\n" +
               $"👨‍⚕️ *Especialista:* {profesional}\n\n" +
               $"📲 Puedes consultar el detalle de tu cita y ubicación en el siguiente enlace:\n{link}\n\n" +
               $"_¡Te esperamos con gusto!_";
    }

    public static string MensajeRecordatorio24h(
        string cliente,
        string servicio,
        string negocio,
        DateTime fechaHoraLocal,
        Guid tokenAcceso,
        string baseUrl = "https://timely.io")
    {
        var fechaStr = fechaHoraLocal.ToString("dddd d 'de' MMMM, h:mm tt");
        var link = $"{baseUrl.TrimEnd('/')}/mi-cita/{tokenAcceso}";

        return $"⏰ *Recordatorio de Cita en {negocio}*\n\n" +
               $"Hola {cliente}, te recordamos que tu consulta de *{servicio}* está programada para mañana:\n\n" +
               $"📅 *{fechaStr}*\n\n" +
               $"Para asegurar tu lugar o gestionar tu turno, por favor confírmanos aquí:\n" +
               $"{link}\n\n" +
               $"¡Gracias por tu confianza!";
    }

    public static string GenerarUrlWhatsApp(string telefono, string mensaje)
    {
        var telLimpio = SanitizarTelefono(telefono);
        var encoded = Uri.EscapeDataString(mensaje);
        return $"https://wa.me/{telLimpio}?text={encoded}";
    }
}
