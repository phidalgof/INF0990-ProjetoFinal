namespace ProjetoFinal;
/// <summary>
/// Classe Radioactive: define elemento radioativo
/// </summary>
public class Radioactive : Radio, Rechargeable 
{
    public Radioactive() : base("!! ") {}
    /// <summary>
    /// Definição do valor de recarga/retirada de energia (-30)
    /// </summary>
    public void Recharge(Robot r)
    {
        r.energy = r.energy -30;
    }
}