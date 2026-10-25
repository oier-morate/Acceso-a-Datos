using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    private static string fichero = null;

    static void Main(string[] args)
    {
        int opcion;
        do
        {
            MostrarMenu();
            Console.Write("Elige una opción: ");
            int.TryParse(Console.ReadLine(), out opcion);
            switch (opcion)
            {
                case 1:
                    CrearFichero(); 
                    break;
                case 2: 
                    AnadirRegistros(); 
                    break;
                case 3: 
                    EliminarRegistros(); 
                    break;
                case 4: 
                    EliminarFichero(); 
                    break;
                case 5: 
                    VisualizarRegistros(); 
                    break;
                case 6: 
                    Console.WriteLine("Cerrando la aplicación. ¡Hasta pronto!"); 
                    break;
                default: 
                    Console.WriteLine("Opción no válida."); 
                    break;
            }
        } while (opcion != 6);
    }

    static void MostrarMenu()
    {
        Console.WriteLine("\n===== GESTOR DE FICHEROS =====");
        Console.WriteLine("Fichero actual: " + (fichero != null ? Path.GetFileName(fichero) : "(ninguno)"));
        Console.WriteLine("1. Crear un nuevo fichero");
        Console.WriteLine("2. Añadir registros al fichero");
        Console.WriteLine("3. Eliminar todos los registros del fichero");
        Console.WriteLine("4. Eliminar el fichero");
        Console.WriteLine("5. Visualizar los registros");
        Console.WriteLine("6. Cerrar la aplicación");
    }

    static void CrearFichero()
    {
        Console.Write("Nombre del fichero (ej. datos.txt): ");
        string nombre = Console.ReadLine().Trim();
        if (nombre == "")
        {
            Console.WriteLine("El nombre no puede estar vacío.");
            return;
        }

        nombre = "./Ficheros/" + nombre;
        try
        {
            if (File.Exists(nombre))
            {
                Console.WriteLine("El fichero ya existía; se usará como fichero actual.");
            }
            else
            {
                File.Create(nombre).Close();
                Console.WriteLine("Fichero creado correctamente.");
            }
            fichero = nombre;
        }
        catch (Exception e)
        {
            Console.WriteLine("Error al crear el fichero: " + e.Message);
        }
    }

    static void AnadirRegistros()
    {
        if (!HayFichero()) 
            return;
        Console.WriteLine("Escribe los registros (una línea por registro). Línea vacía para terminar.");
        try
        {
            int contador = 0;
            using (StreamWriter sw = new StreamWriter(fichero, true))
            {
                while (true)
                {
                    Console.Write("Registro: ");
                    string linea = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(linea)) 
                        break;
                    sw.WriteLine(linea);
                    contador++;
                }
            }
            Console.WriteLine(contador + " registro(s) añadido(s).");
        }
        catch (Exception e)
        {
            Console.WriteLine("Error al escribir en el fichero: " + e.Message);
        }
    }

    static void EliminarRegistros()
    {
        if (!HayFichero()) 
            return;
        try
        {
            File.WriteAllText(fichero, "");
            Console.WriteLine("Se han eliminado todos los registros.");
        }
        catch (Exception e)
        {
            Console.WriteLine("Error al vaciar el fichero: " + e.Message);
        }
    }

    static void EliminarFichero()
    {
        if (!HayFichero()) 
            return;
        try
        {
            File.Delete(fichero);
            Console.WriteLine("Fichero " + fichero + " eliminado.");
            fichero = null;
        }
        catch (Exception e)
        {
            Console.WriteLine("No se ha podido eliminar el fichero: " + e.Message);
        }
    }

    static void VisualizarRegistros()
    {
        if (!HayFichero()) 
            return;
        try
        {
            string[] registros = File.ReadAllLines(fichero);
            if (registros.Length == 0)
            {
                Console.WriteLine("El fichero no tiene registros.");
                return;
            }
            Console.WriteLine("--- Registros ---");
            for (int i = 0; i < registros.Length; i++)
            {
                Console.WriteLine((i + 1) + ": " + registros[i]);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Error al leer el fichero: " + e.Message);
        }
    }

    static bool HayFichero()
    {
        if (fichero == null || !File.Exists(fichero))
        {
            Console.WriteLine("No hay ningún fichero activo. Crea uno primero (opción 1).");
            fichero = null;
            return false;
        }
        return true;
    }
}
