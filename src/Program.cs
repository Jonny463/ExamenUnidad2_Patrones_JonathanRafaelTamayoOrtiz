using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluacionU2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int opc = 0;
            int id = 0;

            do
            {   //Intenta crear una nuava instancia con cada ciclo. Singleton devuelve la ya existente
                RobotPool<Robot> coordinador = RobotPool<Robot>.Instance;
                Console.WriteLine("===Menu principal===\n");
                coordinador.VerificarCoordinador();
                coordinador.ReporteGeneral();
                Console.WriteLine("\n\nEliga una opcion:");
                Console.WriteLine("1.- Activar un robot");
                Console.WriteLine("2.- Desactivar un robot");
                Console.WriteLine("3.- Cambiar la tarea de un robot");
                Console.WriteLine("4.- Ver informe de un robot");
                Console.WriteLine("0.- Salir\n");
                int.TryParse(Console.ReadLine(), out opc);

                switch (opc)
                {
                    case 1: //Activar robot
                        var robot1 = coordinador.ActivarRobot();
                        if (robot1 != null) { coordinador.AsignarTareaManual(robot1.IdRobot); }
                        else { Console.WriteLine("No se pudo encontrar al robot"); }
                        break;
                    case 2: //Devolver robot
                        Console.WriteLine("Ingrese el ID del robot que desea devolver:");
                        int.TryParse(Console.ReadLine(), out id);
                        coordinador.DevolverRobot(id);
                        break;
                    case 3: //Cambiar la tarea de un robot
                        Console.WriteLine("Ingrese el ID del robot al que desea asignar una nueva tarea:");
                        int.TryParse(Console.ReadLine(), out id);
                        var robot3 = coordinador.BuscarRobot(id);
                        if (!robot3.getEstatusActividad()) 
                        {
                            Console.WriteLine("El robot debe ser activado antes de asignarle actividad");
                            break;
                        }
                        if (robot3 != null) { coordinador.AsignarTareaManual(robot3.IdRobot); }
                        else { Console.WriteLine("No se pudo encontrar al robot"); }
                        break;
                    case 4: //Muestra el estado de un solo robot
                        Console.WriteLine("Ingrese el ID del robot al que desea visualizar:");
                        int.TryParse(Console.ReadLine(), out id);
                        var robot4 = coordinador.BuscarRobot(id);
                        if (robot4 != null) { robot4.ReporteRobot(); }
                        else { Console.WriteLine("No se pudo encontrar al robot"); }
                        break;
                    case 0: //Salir
                        break;
                    default:
                        Console.WriteLine("Esa opcion no es valida.");
                        break;
                }
                Console.ReadKey();
                Console.Clear();
            } while (opc != 0);
        }
    }
}
