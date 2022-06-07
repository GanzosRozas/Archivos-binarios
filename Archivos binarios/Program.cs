using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Archivos_binarios
{
    class Program
    {
        class ArichivosBinariosEmpleados
        {
            //Declaracion de flujos
            BinaryWriter bw = null;//flujo salida-escritura de datos
            BinaryReader br = null;//flujo entrada-lectura de datos

            //campos de la clase
            string Nombre, Direccion;
            long Telefono;
            int NumEmp, DiasTrabajados;
            float SalarioDiario;

            public void CrearArchivo(string Archivo)
            {
                //variable local metodo
                char resp;
                try
                {
                    //Creacion del flujo para escribir datos al archivo
                    bw = new BinaryWriter(new FileStream(Archivo, FileMode.Create, FileAccess.Write));
                    
                    //Captura de datos
                    do
                    {
                        Console.Clear();
                        Console.Write("Numero del Empleado: ");
                        NumEmp = Int32.Parse(Console.ReadLine());
                        Console.Write("Nombre del Empleado: ");
                        Nombre = Console.ReadLine();
                        Console.Write("Direccion del Empleado: ");
                        Direccion = Console.ReadLine();
                        Console.Write("Telefono del Empleado: ");
                        Telefono = long.Parse(Console.ReadLine());
                        Console.Write("Dias Trabajados del Empleado: ");
                        DiasTrabajados = int.Parse(Console.ReadLine());
                        Console.Write("Salario Diario del Empleado: ");
                        SalarioDiario = float.Parse(Console.ReadLine());

                        //Escribe los datos del archivo
                        bw.Write(NumEmp);
                        bw.Write(Nombre);
                        bw.Write(Direccion);
                        bw.Write(Telefono);
                        bw.Write(DiasTrabajados);
                        bw.Write(SalarioDiario);

                        Console.Write("\n\nDeseas Almacenar otro Registo(s/n)?");
                        resp = char.Parse(Console.ReadLine());
                    } while (resp=='s'||resp=='S');
                }
                catch (IOException e)
                {

                    Console.WriteLine("\nError : " + e.Message);
                    Console.WriteLine("\nRuta : " + e.StackTrace);
                }
                finally
                {
                    if (bw != null) bw.Close();
                    Console.Write("\nPresione <enter> para terminar la Escritura de Datos y regresar al Menu.");
                    Console.ReadKey();
                }
            }
            public void MostrarArchivo(string Archivo)
            {
                try
                {
                    //Verifica si existe el archivo
                    if (File.Exists(Archivo))
                    {
                        br = new BinaryReader(new FileStream(Archivo, FileMode.Open, FileAccess.Read));

                        //despliegue de datos en pantalla
                        Console.Clear();
                        do
                        {
                            //Lectura de registros mientras no llegue a EndOfFile
                            NumEmp = br.ReadInt32();
                            Nombre = br.ReadString();
                            Direccion = br.ReadString();
                            Telefono = br.ReadInt64();
                            DiasTrabajados = br.ReadInt32();
                            SalarioDiario = br.ReadSingle();

                            //muestra los datos
                            Console.WriteLine("Numero del Elmpleado:    " + NumEmp);
                            Console.WriteLine("Nombre del Elmpleado:    " + Nombre);
                            Console.WriteLine("Direccion del Elmpleado:    " + Direccion);
                            Console.WriteLine("Telefono del Elmpleado:    " + Telefono);
                            Console.WriteLine("Dias trabajados del Elmpleado:    " + DiasTrabajados);
                            Console.WriteLine("Salario Diario del Elmpleado{0:C}    ",  SalarioDiario);
                            Console.WriteLine("SUELDO TOTAL del Empleado:   {0:C}",SalarioDiario*DiasTrabajados);
                            Console.WriteLine("\n");
                        } while (true);
                    }else
                    {
                        Console.Clear();
                        Console.WriteLine("\n\nEl Archivo " + Archivo + " No Existe en el Disco!!");
                        Console.Write("\nPresione <enter> para Continuar...");
                        Console.ReadKey();
                    }
                }
                catch (EndOfStreamException)
                {

                    Console.WriteLine("\n\nFin del Listado de Empleados");
                    Console.Write("\nPresione <enter> para Continuar...");
                    Console.ReadKey();

                }
                finally
                {
                    if (br != null) br.Close();
                    Console.Write("\nPresione <enter> para terminar la Escritura de Datos y regresar al Menu.");
                    Console.ReadKey();
                }
            }
        }
        static void Main(string[] args)
        {
            //declaracion de variables auxiliares
            string arch = null;
            int opcion;

            //creacion del objeto
            ArichivosBinariosEmpleados A1 = new ArichivosBinariosEmpleados();
            //Menu de Opciones
            do
            {
                Console.Clear();
                Console.WriteLine("\n*** ARCHIVO BINARIO EMPLEADOS ***");
                Console.WriteLine("1.- Creacion de un Archivo.");
                Console.WriteLine("2.- Lecutra de un Archivo.");
                Console.WriteLine("3.- Salida del Programa.");
                Console.Write("\nQue opcion deseas: ");
                opcion = Int16.Parse(Console.ReadLine());
                switch (opcion)
                {
                    case 1:
                        //Bloque de escritura
                        try
                        {
                            //captura nombre archivo
                            Console.Write("\nEscribe el nombre del archivo a Crear: ");
                            arch = Console.ReadLine();
                            //verificando si existe el archivo
                            char resp = 's';
                            if (File.Exists(arch))
                            {
                                Console.Write("\nEl Archivo Existe!!, Desea Sobreescribirlo(s/n)? ");
                                resp = Char.Parse(Console.ReadLine());

                            }
                            if (resp=='s'||resp=='S')
                            {
                                A1.CrearArchivo(arch);
                            }
                        }
                        catch (IOException e)
                        {
                            Console.WriteLine("\nError : " + e.Message);
                            Console.WriteLine("\nRuta : " + e.StackTrace);
                        }
                        break;
                    case 2:
                        //bloque de lectura
                        try
                        {
                            //Captura nombre archivo
                            Console.Write("\nEscriba el Nombre del Archivo que deseas Leer: ");
                            arch = Console.ReadLine();
                            A1.MostrarArchivo(arch);
                        }
                        catch (IOException e)
                        {
                            Console.WriteLine("\nError : " + e.Message);
                            Console.WriteLine("\nRuta : " + e.StackTrace);
                        }
                        break;
                    case 3:
                        Console.Write("\nPresione <enter> para Salir del programa.");
                        Console.ReadKey();
                        break;
                       
                    default:
                        Console.Write("\nEsa Opcion No Existe!!, Presione <enter> para Continuar...");
                        Console.ReadKey();
                        break;
                }
            } while (opcion !=3);
        }

    }
}
