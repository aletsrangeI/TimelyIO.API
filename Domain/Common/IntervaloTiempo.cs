namespace Domain.Common;

/// <summary>
/// Value Object inmutable que representa un intervalo de tiempo y encapsula la lógica anti-traslapes.
/// </summary>
public record IntervaloTiempo
{
    public DateTime InicioUtc { get; }
    public DateTime FinUtc { get; }

    public IntervaloTiempo(DateTime inicioUtc, DateTime finUtc)
    {
        if (finUtc <= inicioUtc)
        {
            throw new ArgumentException("La fecha y hora de fin debe ser posterior a la fecha de inicio.", nameof(finUtc));
        }

        InicioUtc = inicioUtc;
        FinUtc = finUtc;
    }

    /// <summary>
    /// Determina si este intervalo se traslapa con otro intervalo dado.
    /// Condición matemática: Inicio1 < Fin2 && Fin1 > Inicio2
    /// </summary>
    public bool SeTraslapaCon(IntervaloTiempo otro)
    {
        if (otro is null) return false;
        return InicioUtc < otro.FinUtc && FinUtc > otro.InicioUtc;
    }

    /// <summary>
    /// Duración total del intervalo en minutos.
    /// </summary>
    public int DuracionMinutos => (int)(FinUtc - InicioUtc).TotalMinutes;
}
