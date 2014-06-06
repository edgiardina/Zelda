using System;
using System.Collections.Generic;
using System.Text;

namespace Test_Game.Physics
{
    public class Spring
    {
        float stiffness = 3600.0f;
        float damping = 500.0f;
        float mass = 100.0f;
        float velocity = 0;

        /// <summary>
        /// Create a Spring object, with default stiffness, damping, mass and initial velocity
        /// </summary>
        public Spring()
        {

        }

        public Spring(float StiffNess, float Damping, float Mass, float Velocity)
        {
            this.stiffness = StiffNess;
            this.damping = Damping;
            this.mass = Mass;
            this.velocity = Velocity;
        }

        /// <summary>
        /// Get new position of object tethered to this spring based on Time passed.
        /// </summary>
        /// <param name="CurrentPosition">Current Position of Object to tether to spring</param>
        /// <param name="DesiredPosition">Desired Position of Object (no spring tension position)</param>
        /// <param name="TotalSecondsPassed">Time elapsed (T) in the Spring Equation</param>
        /// <returns>New Position of Object</returns>
        public float UpdateSprungObject(float CurrentPosition, float DesiredPosition, float TotalSecondsPassed)
        {
            float stretch = CurrentPosition - DesiredPosition;
            float force = (-stiffness * stretch) - (damping * velocity);

            // Apply acceleration
            float acceleration = force / mass;
            velocity += acceleration * TotalSecondsPassed;

            // Apply velocity
            return CurrentPosition + velocity * TotalSecondsPassed;
        }


    }
}
