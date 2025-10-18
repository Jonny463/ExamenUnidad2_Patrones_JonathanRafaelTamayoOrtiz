using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluacionU2
{
    public class RobotPool<T> where T : class, IRobot, new()
    {   //Singleton
        private static RobotPool<T> _instance; //Una sola instancia de la piscina
        private static readonly object _lock = new object();
        public string IdCoordinador { get; set; }


        //Object pool
        public readonly Stack<T> RobotsPool = new Stack<T>(); //Stack de todos los robots a la espera de instrucciones
        private readonly List<T> RobotsActivos = new List<T>(); //Lista de robots en uso
        private int maxPool { get; set; } //Tamaño del pool de robots
        
        private RobotPool(int maxPool)
        {
            IdCoordinador = "12345";
            this.maxPool = maxPool;
            //Instanciar los robots pertenecientes al pool
            for(int i = 1; i <= maxPool; i++)
            {
                var robot = new T();
                robot.IdRobot = i;
                RobotsPool.Push(robot);
                AsignarTareaAuto(robot.IdRobot, 0);
            }
        }
       
        public static RobotPool<T> Instance // Inicializa la instancia del coordinador
        {                                   // Lo cual inicializa la piscina de objetos
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new RobotPool<T>(10);
                            Console.WriteLine("Nueva instancia creada");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Instancia existente devuelta");
                }
                return _instance;
            }
        }
        public void VerificarCoordinador()
        {
            Console.WriteLine($"Coordinador ID: {IdCoordinador}");
        }
        public T ActivarRobot() //Activa un robot
        {
            T robot;
            if(RobotsPool.Count > 0)
            {
                robot = RobotsPool.Pop();
                RobotsActivos.Add(robot);
                ActividadRobot(robot.IdRobot, true);
                Console.WriteLine($"Robot ID: {robot.IdRobot} activado");
                return robot;
            }
            Console.WriteLine("No es posible activar un robot en este momento");
            return null;
        }
        public void ActividadRobot(int id, bool estatus) //Marca un robot como activo
        {
            var robot = BuscarRobot(id);
            if(robot != null && robot is Robot r)
            {
                r.AsignarEstatus(estatus);
            }
        }
        public void AsignarTareaAuto(int id, int tarea) //La tarea se asigna automaticamente por el sistema
        {
            var robot = BuscarRobot(id);
            if (robot != null && robot is Robot r)
            {
                r.AsignarTarea(tarea);
            }
        }
        public void AsignarTareaManual(int id) //La tarea es seleccionada por el usuario
        {
            Console.WriteLine("\nSeleccione una nueva tarea:\n" +
                                $"1.- Limpieza\n2.- Vigilancia\n3.- Paqueteria\n4.- Terminator");

            if (int.TryParse(Console.ReadLine(), out int tarea) && (tarea >= 0 && tarea <= 4))
            {
                var robot = BuscarRobot(id);
                if (robot != null && robot is Robot r)
                {
                    r.AsignarTarea(tarea);
                    Console.WriteLine($"Tarea de la unidad {r.IdRobot}: {r.GetTareaRobot()}");
                }
                else
                {
                    Console.WriteLine("No es posible asignar esa tarea.");
                }
            }
            else
            {
                Console.WriteLine("Opcion invalida.");
            }
        }
        public void DevolverRobot(int Id) //Devuelve el robot al pool
        {
            var robot = RobotsActivos.FirstOrDefault(r => r.IdRobot == Id);
            if (robot != null)
            {
                RobotsActivos.Remove(robot);
                RobotsPool.Push(robot);
                ActividadRobot(robot.IdRobot, false);
                AsignarTareaAuto(robot.IdRobot, 0);
                Console.WriteLine($"Robot ID: {robot.IdRobot} devuelto a la piscina");
            }
            else
            {
                Console.WriteLine("Robot no encontrado");
            }
        }
        public T BuscarRobot(int Id)
        {
            return RobotsPool.Concat(RobotsActivos).FirstOrDefault(r => r.IdRobot == Id);
        }
        public void ReporteGeneral()
        {
            Console.WriteLine("\nEstatus del grupo de robots:\n" +
                $"Robots activos: {RobotsActivos.Count}\n" +
                $"Robots disponibles: {RobotsPool.Count}");
            if (RobotsActivos.Count > 0)
            {   // Muestra los robots que estan realizando alguna tarea, si los hay
                Console.Write("Lista de robots activos: ");
                foreach(var r in RobotsActivos.Cast<Robot>())
                {
                    Console.Write(r.IdRobot + "\t");
                }
            }
        }
    }
}
