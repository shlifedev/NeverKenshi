namespace Kenshi
{
    public struct HitInfo
    {
        public Entity attacker;
        public Entity victim;
        public int damage;
    }
    public interface IHitable
    { 
        void Hit(HitInfo info);
    }
    
    
}