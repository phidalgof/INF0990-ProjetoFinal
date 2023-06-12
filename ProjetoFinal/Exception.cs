namespace ProjetoFinal;
/// <summary>
/// Classe Exceptions: define exceções no preenchimento dos items no mapa
/// e no deslocamento do robo
/// </summary>
public class OutOfMapException : Exception {}
public class OccupiedPositionException : Exception {}
public class RanOutOfEnergyException : Exception {}
