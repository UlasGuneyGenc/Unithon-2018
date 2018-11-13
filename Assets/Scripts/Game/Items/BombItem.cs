using DG.Tweening;
using Game.Core.Item;
using UnityEditor.VersionControl;
using UnityEngine;

namespace Game.Items
{
    public class BombItem : Item
    {
        private ItemType _itemType;
        private ParticleSystem _particleSystem;

        public override ItemType GetItemType()
        {
            return ItemType.Bomb;
        }        
        
        public override bool IsMatchable()
        {
            return true;
        }
        public override bool IsFallable()
        {
            return true;
        }

        public override bool TryExecute()
        {

            if (_particleSystem == null) return base.TryExecute();
            
            var particle = Instantiate(
                _particleSystem, transform.position, Quaternion.identity, Cell.Board.ParticlesParent);
            particle.Play();
            
            return base.TryExecute();
        }
        public void SetParticle(ParticleSystem particles)
        {
            _particleSystem = particles;
        }
    }
}
