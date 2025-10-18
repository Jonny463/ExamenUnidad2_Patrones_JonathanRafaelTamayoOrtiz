using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluacionU2
{
    public class Robot : IRobot
    {
        public int IdRobot { get; set; }
        public enum TareaRobot { Inactivo = 0, Limpieza = 1, Vigilancia = 2, Paqueteria = 3, Terminator = 4 }
        public TareaRobot Tarea;  
        protected bool EstatusActividad = false; //El robot esta activo (true) o inactivo (false)

        public TareaRobot GetTareaRobot() //Devuelve la tarea que esta realizando un robot
        {
            return Tarea;
        }
        public bool getEstatusActividad() //Devuelve un valor booleano
        { 
            return EstatusActividad;
        }
        public string isActivo() //Devuelve un valor string
        {
            if(getEstatusActividad() == true) //activo = (true) e inactivo = (false)
            {
                return "Activo";
            }
            return "Inactivo";
        }
        public void AsignarTarea(int tipo)
        {
            if (getEstatusActividad() == true) // Si el robot esta acivo, asigna la tarea que corresponde
            {
                this.Tarea = (TareaRobot)tipo;
            }
            else // Si el robot esta inactivo, lo marca como 0
            {
                this.Tarea = (TareaRobot)0;
            }
        }
        public void AsignarEstatus(bool estatus)
        {
            EstatusActividad = estatus;
        }
        public void ReporteRobot()
        {
            Console.WriteLine($"\nRobot ID: {IdRobot}\nTarea: {GetTareaRobot()}\nEstatus: {isActivo()}");
        }
    }
}
