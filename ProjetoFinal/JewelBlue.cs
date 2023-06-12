namespace ProjetoFinal;
/// <summary>
/// Classe JewelBlue: define Jewel Blue e o valor de recarga de energia (5)
/// </summary>
public class JewelBlue : Jewel, Rechargeable 
{
    
    public void Recharge(Robot r)
    {
        r.energy = r.energy + 5;
    }
    /// <summary>
    /// Definição de Jewel Blue e a sua pontuação (10)
    /// </summary>
    public JewelBlue() : base("JB ", 10) {}
}