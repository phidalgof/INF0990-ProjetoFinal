namespace ProjetoFinal;
/// <summary>
/// Classe Robot: define posição do robô
/// Bag: quantidade de joias coletadas
/// métodos de interação com o mapa (deslocamento e coleta de joias)
/// imprime o total de joias coletadas e pontuação atual
/// </summary>
public class Robot: ItemMap
{
    public Map map {get; private set;}
    private int x, y;
    private List<Jewel> Bag = new List<Jewel>();
    public int energy {get; set;}
    
    /// <summary>
    /// Posição inicial do robo, energia atual e nível de jogo.
    /// </summary>
    public Robot(Map map, int x=0, int y=0, int energy=5) : base("ME ")
    {
        this.map = map;
        this.x = x;
        this.y = y;
        this.energy = energy;
        this.map.Insert(this, x, y);
    }
    /// <summary>
    /// método de deslocamento. Up / Down / Right / Left
    /// tratamentos de exceptions
    /// </summary>
    public void MoveUp(){
        try
        {
            map.Update(this.x, this.y, this.x-1, this.y);
            this.x--;
            this.energy--;
        }
        catch (OccupiedPositionException exceptions)
        {
            Console.WriteLine($"\nPosição ({this.x-1}, {this.y}) ocupada");
        }
        catch (OutOfMapException exceptions)
        {
            Console.WriteLine($"\nPosição ({this.x-1}, {this.y}) fora do mapa");
        }
    }
    public void MoveDown(){
        try
        {
            map.Update(this.x, this.y, this.x+1, this.y);
            this.x++;
            this.energy--;
        }
        catch (OccupiedPositionException exceptions)
        {         
            Console.WriteLine($"\nPosição ({this.x+1}, {this.y}) ocupada");
        }
        catch (OutOfMapException exceptions)
        {         
            Console.WriteLine($"\nPosição ({this.x+1}, {this.y}) fora do mapa");
        }
    }
    public void MoveRight(){
        try
        {
            map.Update(this.x, this.y, this.x, this.y+1);
            this.y++;
            this.energy--;
        }
        catch (OccupiedPositionException exceptions)
        {         
            Console.WriteLine($"\nPosição ({this.x}, {this.y+1}) ocupada");
        }
        catch (OutOfMapException exceptions)
        {        
            Console.WriteLine($"\nPosição ({this.x}, {this.y+1}) fora do mapa");
        }
    }
    public void MoveLeft(){
        try
        {
            map.Update(this.x, this.y, this.x, this.y-1);
            this.y--;
            this.energy--;
        }
        catch (OccupiedPositionException exceptions)
        {         
            Console.WriteLine($"\nPosição ({this.x}, {this.y-1}) ocupada");
        }
        catch (OutOfMapException exceptions)
        {            
            Console.WriteLine($"\nPosição ({this.x}, {this.y-1}) fora do mapa");
        }
    }
    /// <summary>
    /// método de recarga de energia do robo e coleta de joias
    /// </summary>
    public void Get()
    {
        Rechargeable? RechargeEnergy = map.GetRechargeable(this.x, this.y);
        RechargeEnergy?.Recharge(this);
        List<Jewel> NearJewels = map.GetJewels(this.x, this.y);
        List<Radio> NearRadios = map.GetRadios(this.x, this.y);
        foreach (Jewel j in NearJewels)
            Bag.Add(j);
    }
    /// <summary>
    /// pontuação atual
    /// </summary>
    private (int, int) GetBagInfo()
    {
        int Points = 0;
        foreach (Jewel j in this.Bag)
            Points += j.Points;
        return (this.Bag.Count, Points);
    }
    /// <summary>
    /// quantidade de items coletados, pontos e energia atual.
    /// </summary>
    public void PrintMap()
    {
        map.PrintMap();
        (int ItensBag, int TotalPoints) = this.GetBagInfo();
        Console.WriteLine($"\nItens Bag: {ItensBag} - Total Pontos: {TotalPoints} - Energia: {this.energy} - x:{this.x}, y: {this.y}\n\n");
    }
    public bool HasEnergy()
    {
        return this.energy > 0;
    }
}
