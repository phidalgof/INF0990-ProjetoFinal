namespace ProjetoFinal;
/// <summary>
/// Classe Tree: define Tree e o valor de recarga de energia (3)
/// </summary>
public class Tree : Obstacle, Rechargeable 
{
    public Tree() : base("$$ ") {}
    /// <summary>
    /// Definição do valor de recarga (3)
    /// </summary>
    public void Recharge(Robot r)
    {
        r.energy = r.energy + 3;
    }
}