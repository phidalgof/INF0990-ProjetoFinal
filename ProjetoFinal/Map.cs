namespace ProjetoFinal;
/// <summary>
/// Classe Map: define o mapa 2D
/// método para imprimir o mapa na tela
/// método para imprimir items no mapa
/// </summary>
public class Map
{
    private ItemMap[,] Matriz;
    public int h {get; private set;}
    public int w {get; private set;}

    /// <summary>
    /// Classe criada pra geração do mapa inicial 10 x 10
    /// Para futuros níveis, incrementa +1 o tamanho do mapa, 
    /// Ultmo nível: tamanho limite do mapa 30 x 30.
    /// </summary>
    public Map (int w=10, int h=10, int level=1)
    {
        this.w = w <= 30 ? w : 30;
        this.h = h <= 30 ? h : 30;
        Matriz = new ItemMap[w, h];
        for (int i = 0; i < Matriz.GetLength(0); i++) {
            for (int j = 0; j < Matriz.GetLength(1); j++) {
                Matriz[i, j] = new Empty();
            }
        }
        /// <summary>
        /// Geração do mapa dependendo o nível do jogo.
        /// </summary>
        if (level == 1) 
        {
            GenerateFixed();
        }
        else 
        {
            GenerateRandom();
        }
    }
    /// <summary>
    /// Posicionamento de itens no mapa.
    /// </summary>
    public void Insert (ItemMap Item, int x, int y)
    {
        Matriz[x, y] = Item;
    }
    /// <summary>
    /// Atualicação do mapa apos deslocamento
    /// </summary>
    public void Update(int x_old, int y_old, int x, int y)
    {
        if (x < 0 || y < 0 || x> this.w-1 || y> this.h-1)
        {        
            throw new OutOfMapException();
        }
        if (IsAllowed(x, y))
        {
            Matriz[x, y] = Matriz[x_old, y_old];
            Matriz[x_old, y_old] = new Empty();
        }
        else
        {
            throw new OccupiedPositionException();
        }
    }
    /// <summary>
    /// coletar joias se tiver na vizinhança
    /// </summary>
    public List<Jewel> GetJewels(int x, int y)
    {
        List<Jewel> NearJewels = new List<Jewel>();
        int[,] Coords = GenerateCoord(x, y);
        for (int i = 0; i < Coords.GetLength(0); i++)
        {
            Jewel? jewel = GetJewel(Coords[i, 0], Coords[i, 1]);
            if (jewel is not null) NearJewels.Add(jewel);
        }
        return NearJewels;
    }
    /// <summary>
    /// Efeito se tiver elemento radioativo na vizinhança
    /// </summary>


    /// <summary>
    /// Atualização do espaço da joia coletada para espaço vazio.
    /// </summary>
    private Jewel? GetJewel(int x, int y)
    {
        if (Matriz[x, y] is Jewel jewel)
        {
            Matriz[x, y] = new Empty();
            return jewel;
        }
        return null;
    }
    /// <summary>
    /// Incrementar a energia do robô conforme itens coletados.
    /// </summary>
    public Rechargeable? GetRechargeable(int x, int y){
        int[,] Coords = GenerateCoord(x, y);
        for (int i = 0; i < Coords.GetLength(0); i++)
            if (Matriz[Coords[i, 0], Coords[i, 1]] is Rechargeable r) return r;
        return null;
    }
    /// <summary>
    /// Responsável por gerar coordenadas vizinhas onde pode ser realizado coleta.
    /// </summary>
    private int[,] GenerateCoord(int x, int y)
    {
        int[,] Coords = new int[4, 2]{
            {x,  y+1 < w-1 ? y+1 : w-1},
            {x, y-1 > 0 ? y-1 : 0},
            {x+1 < h-1 ? x+1 : h-1, y},
            {x-1 > 0 ? x-1 : 0, y}
        };
        return Coords;
    }
    /// <summary>
    /// Verificar se a posição está vazia
    /// </summary>
    private bool IsAllowed(int x, int y)
    {
        return Matriz[x, y] is Empty;
    }
    public void PrintMap() {
        for (int i = 0; i < Matriz. GetLength(0); i++){
            for (int j = 0; j < Matriz.GetLength(1); j++){
                Console.Write(Matriz[i, j]);
            }
            Console.Write("\n");
        }
    }
    public bool IsDone()
    {
        for (int i = 0; i < Matriz.GetLength(0); i++) {
            for (int j= 0; j < Matriz.GetLength(1); j++){
                if (Matriz[i, j] is Jewel) return false;
            }
        }
        return true;
    }
    /// <summary>
    /// Configuração inicial: mapa com joias, água e árvores da tela inicial.
    /// </summary>
    private void GenerateFixed()
    {
        this.Insert(new JewelRed(), 1, 9);
        this.Insert(new JewelRed(), 8, 8);
        this.Insert(new JewelGreen(), 9, 1);
        this.Insert(new JewelGreen(), 7, 6);
        this.Insert(new JewelBlue(), 9, 4);
        this.Insert(new JewelBlue(), 2, 1);

        this.Insert(new Water(), 5, 0);
        this.Insert(new Water(), 5, 1);
        this.Insert(new Water(), 5, 2);
        this.Insert(new Water(), 5, 3);
        this.Insert(new Water(), 5, 4);
        this.Insert(new Water(), 5, 5);
        this.Insert(new Water(), 5, 6);
        this.Insert(new Tree(), 5, 9);
        this.Insert(new Tree(), 3, 9);
        this.Insert(new Tree(), 8, 3);
        this.Insert(new Tree(), 2, 5);
        this.Insert(new Tree(), 1, 4);
    }
    /// <summary>
    /// Preenchimento de mapa: usado para mapas do nível 2 em diante.
    /// </summary>
    private void GenerateRandom()
    {
        Random r = new Random();
        for(int x = 0; x < 3; x++)
        {
            int xRandom = r.Next(0, w);
            int yRandom = r.Next(0, h);
            this.Insert(new JewelBlue(), xRandom, yRandom);
        }
        for(int x = 0; x < 3; x++)
        {
            int xRandom = r.Next(0, w);
            int yRandom = r.Next(0, h);
            this.Insert(new JewelGreen(), xRandom, yRandom);
        }
        for(int x = 0; x < 3; x++)
        {
            int xRandom = r.Next(0, w);
            int yRandom = r.Next(0, h);
            this.Insert(new JewelRed(), xRandom, yRandom);
        }
        for(int x = 0; x < 10; x++)
        {
            int xRandom = r.Next(0, w);
            int yRandom = r.Next(0, h);
            this.Insert(new Water(), xRandom, yRandom);
        }
        for(int x = 0; x < 10; x++)
        {
            int xRandom = r.Next(0, w);
            int yRandom = r.Next(0, h);
            this.Insert(new Tree(), xRandom, yRandom);
        }
        for(int x = 0; x < 1; x++)
        {
            int xRandom = r.Next(0, w);
            int yRandom = r.Next(0, h);
            this.Insert(new Radioactive(), xRandom, yRandom);
        }
    }

}
