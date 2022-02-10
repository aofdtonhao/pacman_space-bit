namespace Tonhex
{

    public class PowerPellet : Pellet
    {

        public float duration = 8f;

        public override void Scored()
        {
            GameManager.Instance.PowerPelletEaten(this);
        }

    }

}
