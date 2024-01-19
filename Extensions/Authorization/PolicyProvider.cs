/* Proveedor de politicas de autorizacion para definir reglas de acceso, enfocado a roles */
/* Clase de ejemplo falta personalizar */
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Extensions.Authorization
{
    public class PolicyProvider : IAuthorizationPolicyProvider
    {
        private readonly AuthorizationOptions _options;

        public PolicyProvider(IOptions<AuthorizationOptions> options)
        {
            _options = options.Value;
        }

        public Task<AuthorizationPolicy> GetDefaultPolicyAsync()
        {
            return Task.FromResult(_options.DefaultPolicy);
        }

        public Task<AuthorizationPolicy> GetFallbackPolicyAsync()
        {
            return Task.FromResult(_options.FallbackPolicy);
        }

        public Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
        {
            if (IsCustomPolicy(policyName))
            {
                var policy = new AuthorizationPolicyBuilder();
                policy.RequireAuthenticatedUser(); // Ejemplo: requerir que el usuario esté autenticado

                // Aquí puedes añadir lógica para definir reglas de autorización personalizadas
                // Por ejemplo:
                // policy.RequireRole("Admin"); // Requiere el rol "Admin"

                return Task.FromResult(policy.Build());
            }

            return Task.FromResult<AuthorizationPolicy>(null);
        }

        private bool IsCustomPolicy(string policyName)
        {
            // Lógica para verificar si el nombre de la política es personalizado
            return policyName.StartsWith("CustomPolicy:", StringComparison.OrdinalIgnoreCase);
        }
    }
}
