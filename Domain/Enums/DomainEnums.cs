namespace Domain.Enums;

public enum EstadoCita
{
    PendienteValidacionPago = 1,
    Confirmada = 2,
    EnAtencion = 3,
    Completada = 4,
    Cancelada = 5,
    NoShow = 6
}

public enum OrigenCancelacion
{
    Cliente = 1,
    Profesional = 2,
    SistemaPorExpiracion = 3
}

public enum MetodoPago
{
    PasarelaDigital = 1,
    TransferenciaSPEI = 2,
    PagoPresencialInSitu = 3
}

public enum EstadoPago
{
    PendienteValidacion = 1,
    Aprobado = 2,
    Rechazado = 3,
    Reembolsado = 4
}

public enum TipoRemitente
{
    Profesional = 1,
    Cliente = 2,
    SistemaBot = 3
}
