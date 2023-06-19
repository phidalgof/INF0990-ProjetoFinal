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
	/// Propriedade que determina estado do jogo
	/// </summary>
    public static bool Running { get; set; }
    /// <summary>
    /// Método principal: inicio o jogo.
    /// </summary>
    public static void Main() {
        Running = true;
        int w = 10;
        int h = 10;
        int level = 1;
        Map map = new Map (w, h, level);
        Robot robot = new Robot(map);
        while(Running)
        {
            robot.UpdateRobot(w, h, level);
            Console.WriteLine("\n* * * * JEWEL COLLECTOR * * * *");
            Console.WriteLine($"Level: {level}\n");
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
            catch(RanOutOfEnergyException exceptions)
            {
                Console.WriteLine("Robô sem energia!");
                Console.WriteLine("* * * Game Over * * *");
                Running = false;
            }
        }
    }
    /// <summary>
    /// Interação com teclado: tratamentos de eventos.
    /// </summary>
    private static bool Run(Robot robot)  
    {
        OnMoveUp = robot.MoveUp;
        OnMoveDown = robot.MoveDown;
        OnMoveRight = robot.MoveRight;
        OnMoveLeft = robot.MoveLeft;
        Get = robot.Get;

        do {
            if(!robot.HasEnergy()) throw new RanOutOfEnergyException();
            robot.PrintMap();
            Console.WriteLine("Esperando comandos ...: ");
            ConsoleKeyInfo command = Console.ReadKey(true);

            switch (command.Key.ToString())
            {
                case "W": 
                    Console.WriteLine($"\nMoving Up * * * Comando:{command.Key.ToString()}\n"); 
                    OnMoveUp(); 
                    break;
                case "S" : 
                    Console.WriteLine($"\nMoving Down * * * Comando:{command.Key.ToString()}\n"); 
                    OnMoveDown(); 
                    break;
                case "D" : 
                    Console.WriteLine($"\nMoving Right * * * Comando:{command.Key.ToString()}\n"); 
                    OnMoveRight(); 
                    break;
                case "A" : 
                    Console.WriteLine($"\nMoving Left * * * Comando:{command.Key.ToString()}\n"); 
                    OnMoveLeft(); 
                    break;
                case "G" : 
                    Console.WriteLine($"\nColecting * * * Comando:{command.Key.ToString()}\n"); 
                    Get(); 
                    break;
                case "Q" :
                    Console.WriteLine("\n* * * GAME OVER * * *\n");
                    return false;
                default: 
                    Console.WriteLine($"\nComando inválido:{command.Key.ToString()}\n"); 
                    break;
            }
        } while (!robot.map.IsDone());
        return true;
    }
}
