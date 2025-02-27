using UnityEngine;

public class SpawnerBomb : Spawner<Bomb>
{
    [SerializeField] private SpawnerCube _spawnerCube;

    private Vector3 _bombPosition;
    
    private void OnEnable()
    {
        _spawnerCube.Falled += Create;
    }

    private void OnDisable()
    {
        _spawnerCube.Falled -= Create;
    }

    protected override void GetAction(Bomb bomb)
    {
        bomb.transform.position = _bombPosition;
        
        bomb.ReturnColorAlfa();

        bomb.Exploded += ReturnInPool;
        
        base.GetAction(bomb);
    }

    protected override void ReturnInPool(Bomb bomb)
    {
        bomb.Exploded -= ReturnInPool;
        
        Release(bomb);
        
        base.ReturnInPool(bomb);
    }
    
    private void Create(Cube cube)
    {
        _bombPosition = cube.transform.position;

        GetGameObject();
    }
}