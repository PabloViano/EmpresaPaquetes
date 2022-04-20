using Entidades;
using Newtonsoft;
using Newtonsoft.Json;

namespace Logica
{
    public class Empresa
    {
        List<Envio> Envios = new List<Envio>();
        List<Persona> Personas = new List<Persona>();

        public string DarAltaEnvio (int dniDestinatario, DateTime fechaEstimada, string descripcion, decimal costo)
        {
            string Validaciones = "";
            Persona? destinatario = Personas.Find(x => x.DNI == dniDestinatario);
            if (destinatario != null)
            {
                if (destinatario.Telefono != 0 || destinatario.Telefono != null)
                {
                    Envio newEnvio = new Envio((Envios.Count() + 1).ToString(), destinatario as Destinatario, null, fechaEstimada, descripcion, costo);
                    return $"Numero de envio: {(Envios.Count() + 1).ToString()} (201)";
                }
                else
                {
                    Validaciones += "Campo obligatorio no completado (Telefono)";
                }
            }
            else
            {
                Validaciones += " No se encontro el destinatario";
            }
            return "400";
        }
        public bool AcutalizarEstadoEnvio(string codigoSeguimiento, Estados estadoSiguiente)
        {
            Envio? envio = Envios.Find(x => x.Numero == codigoSeguimiento);
            if (envio != null)
            {
                switch (estadoSiguiente)
                {
                    case Estados.ASIGNADO_REPARTIDOR:
                        {
                            if (envio.Estado == Estados.PENDIENTE)
                            {
                                envio.Estado = estadoSiguiente;
                                return true;
                            }
                        }
                        break;
                    case Estados.EN_CAMINO:
                        {
                            if (envio.Estado == Estados.ASIGNADO_REPARTIDOR)
                            {
                                envio.Estado = estadoSiguiente;
                                return true;
                            }
                        }
                        break;
                    case Estados.ENTREGADO:
                        {
                            envio.FechaEstimada = DateTime.Now.Date;
                            envio.ComisionRepartidor = envio.Costo * envio.Repartidor.Comision / 100;
                            envio.Estado = estadoSiguiente;
                            return true;
                        }
                    default:
                        {
                            throw new Exception("Estado no valido");
                        }
                }
            }
            throw new Exception("Envio no encontrado");
        }
        public Repartidor AsigarRepartidor (Envio envio)
        {
            Repartidor? repartidorAsignado = null;
            double distanciamin = double.MaxValue;
            foreach (Persona item in Personas)
            {
                if (item is Repartidor)
                {
                    if (item.CalcularDistancia(envio.Destinatario) <= distanciamin)
                    {
                        distanciamin = item.CalcularDistancia(envio.Destinatario);
                        repartidorAsignado = item as Repartidor;
                    }
                }
            }
            envio.Repartidor = repartidorAsignado;
            return repartidorAsignado;
        }
    }
}