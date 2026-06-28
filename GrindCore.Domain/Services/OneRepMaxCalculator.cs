namespace GrindCore.Domain.Services;

public static class OneRepMaxCalculator
{
    public static double CalculateWithRpe(double weight, int reps, int rpe)
    {
        // Fórmula de Epley (mantiene la base del cálculo)
        double epley1RM = weight * (1 + reps / 30.0);
        
        // Factores ajustados para Powerlifting Avanzado
        // RPE 9 ahora tiene una penalización mínima (0.99), 
        // lo cual es más preciso para alguien de tu nivel.
        double rpeFactor = rpe switch
        {
            10 => 1.00,
            9  => 0.99, // Antes era 0.96 (Sube tu estimación ~7kg)
            8  => 0.96, // Antes era 0.92
            7  => 0.93, // Antes era 0.88
            6  => 0.90, // Añadido para mayor precisión
            _  => 0.85
        };

        return epley1RM * rpeFactor;
    }
}