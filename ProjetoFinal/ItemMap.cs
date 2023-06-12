namespace ProjetoFinal;
/// <summary>
/// Classe ItemMap: define item e sobreescrita para diferentes items no mapa
/// </summary>
public abstract class ItemMap {
    private string Symbol;
    public ItemMap(string Symbol)
    {
        this.Symbol = Symbol;
    }
    public sealed override string ToString()
    {
        return Symbol;
    }
}