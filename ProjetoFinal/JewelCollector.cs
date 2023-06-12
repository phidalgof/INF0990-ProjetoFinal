namespace ProjetoFinal;
/// <summary>
/// Classe JewelCollector: define a função principal do jogo
/// definição de movimentos e o seu controle
/// completando exemplo disponibilizado no git
/// </summary>
public class JewelCollector {
    delegate void MoveUp();
    delegate void MoveDown();
    delegate void MoveRight();
    delegate void MoveLeft();
    delegate void WhenGet();

    static event MoveUp OnMoveUp;
    static event MoveDown OnMoveDown;
    static event MoveRight OnMoveRight;
    static event MoveLeft OnMoveLeft;
    static event WhenGet Get;
/// <summary>
/// Método principal: inicio o jogo.
/// </summary>
    public static void Main() {
        int w = 10;
        int h = 10;
        int level = 1;
        while(true)
        {
            Map map = new Map (w, h, level);
            Robot robot = new Robot(map);

            Console.WriteLine($"Level: {level}");

            try{
                bool Result = Run(robot);
                if(Result)
                {
                    w++;
                    h++;
                    level++;
                }
                else
                {
                    break;
                }
            }
            catch(RanOutOfEnergyException ex)
            {
                Console.WriteLine("Robo sem energia!");
            }
        }
    }
    /// <summary>
    /// Interação com teclado: tratamentos de eventos.
    /// </summary>
    private static bool Run(Robot robot)  
    {
        OnMoveUp += robot.MoveUp;
        OnMoveDown += robot.MoveDown;
        OnMoveRight += robot.MoveRight;
        OnMoveLeft += robot.MoveLeft;
        Get += robot.Get;

        do {
            if(!robot.HasEnergy()) throw new RanOutOfEnergyException();
            robot.PrintMap();
            Console.WriteLine("\n Enter the command: ");
            ConsoleKeyInfo command = Console.ReadKey(true);

            switch (command.Key.ToString())
            {
                case "W": Console.WriteLine($"\n Comando:{command.Key.ToString()}"); OnMoveUp() ; break;
                case "S" : Console.WriteLine($"\n Comando:{command.Key.ToString()}"); OnMoveDown() ; break;
                case "D" : Console.WriteLine($"\n Comando:{command.Key.ToString()}"); OnMoveRight() ; break;
                case "A" : Console.WriteLine($"\n Comando:{command.Key.ToString()}"); OnMoveLeft() ; break;
                case "G" : Console.WriteLine($"\n Comando:{command.Key.ToString()}"); Get() ; break;
                case "Q" : return false;
                default: Console.WriteLine($"\n Comando inválido:{command.Key.ToString()}"); break;
            }
        } while (!robot.map.IsDone());
        return true;
    }
}
