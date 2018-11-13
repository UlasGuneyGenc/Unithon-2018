using Game.Items;
using Settings;
using UnityEngine;

namespace Game.Core.Item
{
    public static class ItemFactory
    {
        private static GameObject _itemBasePrefab;
        private static ImageLibrary _imageLibrary;
        private static ParticleLibrary _particleLibrary;

        public static void Prepare(ImageLibrary imageLibrary, ParticleLibrary particleLibrary)
        {
            _imageLibrary = imageLibrary;
            _particleLibrary = particleLibrary;
        }
        
        public static Item CreateItem(ItemType itemType, Transform parent)
        {
            if (_itemBasePrefab == null)
            {
                _itemBasePrefab = Resources.Load("ItemBase") as GameObject;
            }
            
            var itemBase = GameObject.Instantiate(
                _itemBasePrefab, Vector3.zero, Quaternion.identity, parent).GetComponent<ItemBase>();
            
            Item item = null;
            switch (itemType)
            {
                case ItemType.GreenCube:
                    item = CreateCubeItem(itemType, itemBase, _imageLibrary.GreenCubeSprite, _particleLibrary.CubeGreenParticle);
                    break;
                case ItemType.YellowCube:
                    item = CreateCubeItem(itemType, itemBase, _imageLibrary.YellowCubeSprite, _particleLibrary.CubeYellowParticle);
                    break;
                case ItemType.BlueCube:
                    item = CreateCubeItem(itemType, itemBase, _imageLibrary.BlueCubeSprite, _particleLibrary.CubeBlueParticle);
                    break;
                case ItemType.RedCube:
                    item = CreateCubeItem(itemType, itemBase, _imageLibrary.RedCubeSprite, _particleLibrary.CubeRedParticle);
                    break;    
                case ItemType.Crate:
                    item = CreateCrateItem(itemBase, _imageLibrary.CrateLayer2Sprite,_imageLibrary.CrateLayer1Sprite);
                    break;
                case ItemType.Balloon:
                    item = CreateBalloonItem(itemBase, _imageLibrary.BalloonSprite);
                    break;
                case ItemType.Bomb:
                    item = CreateBombItem(itemBase, _imageLibrary.BombSprite);
                    break;

                default:
                    Debug.LogWarning("Can not create item: "+itemType);
                    break;
            }
            
            return item;
        }

        private static Item CreateCubeItem(ItemType itemType, ItemBase itemBase, Sprite sprite, ParticleSystem particleSystem)
        {
            var cubeItem = itemBase.gameObject.AddComponent<CubeItem>();
            //cubeItem.Prepare(itemBase, sprite);

			if(sprite == _imageLibrary.YellowCubeSprite)
				cubeItem.PrepareHintItem (itemBase, sprite, _imageLibrary.YellowCubeBombHintSprite);	
			
			if(sprite == _imageLibrary.BlueCubeSprite)
				cubeItem.PrepareHintItem (itemBase, sprite, _imageLibrary.BlueCubeBombHintSprite);	
			
			if (sprite == _imageLibrary.GreenCubeSprite)
				cubeItem.PrepareHintItem (itemBase, sprite, _imageLibrary.GreenCubeBombHintSprite);			

			if(sprite == _imageLibrary.RedCubeSprite)
				cubeItem.PrepareHintItem (itemBase, sprite, _imageLibrary.RedCubeBombHintSprite);	

            cubeItem.SetCubeType(itemType);
            cubeItem.SetParticle(particleSystem);
            
            return cubeItem;
        }
        
        private static Item CreateCrateItem(ItemBase itemBase, Sprite crateLock, Sprite crateOpen)
        {
            var crateItem = itemBase.gameObject.AddComponent<CrateItem>();
            crateItem.PrepareCrateItem(itemBase, crateLock,crateOpen);
            return crateItem;
        }
        private static Item CreateBalloonItem(ItemBase itemBase, Sprite balloon)
        {
            var balloonItem = itemBase.gameObject.AddComponent<BalloonItem>();
            balloonItem.Prepare(itemBase, balloon);
            return balloonItem;
        }

        private static Item CreateBombItem(ItemBase itemBase, Sprite bombSprite)
        {
            var bombItem = itemBase.gameObject.AddComponent<BombItem>();
            bombItem.Prepare(itemBase, bombSprite);
            return bombItem;
        }


    }
}


