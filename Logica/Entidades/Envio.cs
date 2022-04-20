using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Envio
    {
        public string Numero { get; set; }
        public Destinatario? Destinatario { get; set; }
        public Repartidor? Repartidor { get; set; }
        public Estados Estado { get; set; }
        public DateTime FechaEstimada { get; set; }
        public string Descripcion { get; set; }
        public decimal Costo { get; set; }
        public decimal? ComisionRepartidor { get; set; }

        public Envio() { }
        public Envio(string numero, Destinatario destinatario, Repartidor repartidor, DateTime fechaEstimada, string desc, decimal costo)
        {
            this.Numero = numero;
            this.Destinatario = destinatario;
            this.Repartidor = repartidor;
            this.FechaEstimada = fechaEstimada;
            this.Estado = Estados.PENDIENTE;
            this.Descripcion = desc;
            this.Costo = costo;
            this.ComisionRepartidor = null;
        }
    }
    public enum Estados { PENDIENTE, ASIGNADO_REPARTIDOR , EN_CAMINO, ENTREGADO}
}
