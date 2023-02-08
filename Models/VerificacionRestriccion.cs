using BancoApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BankApp.Models
{
    public class VerificacionRestriccion : IHostedService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private Timer _timer;

        public VerificacionRestriccion(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }


        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(Verificacion, null, TimeSpan.Zero, TimeSpan.FromMinutes(10));
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        private  void Verificacion(object state)
        {
            if (DateTime.Now.Hour == 12 && DateTime.Now.Minute == 8)
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<BankContext>();

                    var tarjetas = context.tarjetaCreditos.Include(x=> x.Paquete).Where(x => x.limiteCredito > 10000 || (x.Paquete.esCrediticio && x.Paquete.tarjetas.Count() > 3));
                    foreach (var tarjeta in tarjetas)
                    {
                        var client = context.Clientes.Include(c => c.Restriccion).FirstOrDefault(x => x.IdCliente == tarjeta.ProductoId);
                        if (client != null && client.Restriccion == null)
                        {
                            Restriccion restriccion = new Restriccion()
                            {
                                descripcion = "Limite de credito excedido/Maxima cantidad de tarjetas Excedidas para un paqueteCrediticio",
                                fecha = DateTime.Now,
                                ClienteId = client.IdCliente
                            };

                            context.Restricciones.Add(restriccion);
                            context.SaveChanges();

                        }
                    }
                    context.SaveChanges();
                }
                
            }
            
        }

    }
}
