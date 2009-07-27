using System.Reflection;
using Products.Core.Domain;

namespace Products.Presentation.Configuration
{
    public static class Assemblies
    {
        public static readonly Assembly PresentationAssembly = typeof (Assemblies).Assembly;
        public static readonly Assembly CoreAssembly = typeof (Product).Assembly;
    }
}