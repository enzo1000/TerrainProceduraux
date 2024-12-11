using UnityEngine;

public class ProceduralTerrain : MonoBehaviour
{
    public GameObject planeTerrain;

    [Range(0.0f, 10.0f)]
    public float resolution = 1;
    
    private float _width;
    private float _height;
    private float[,] _elevations;
    private FastNoiseLite _noise;

    private bool _debug = false;
    
    private void Start()
    {
        _width = planeTerrain.GetComponent<MeshFilter>().mesh.bounds.size.x;
        _height = planeTerrain.GetComponent<MeshFilter>().mesh.bounds.size.z;
        
        _noise = new FastNoiseLite();
        _noise.SetNoiseType(FastNoiseLite.NoiseType.OpenSimplex2);  //Perlin is fine too
        _noise.SetSeed(Random.Range(0, int.MaxValue));
        
        _elevations = new float[Mathf.RoundToInt(_width), Mathf.RoundToInt(_height)];
    }

    private void Update()
    {
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                //nx, ny define from -0.5 to 0.5
                float nx = x / _width - 0.5f, ny = y / _height - 0.5f;
                _elevations[x,y] = _noise.GetNoise(x, y);
            }
        }

        if (!_debug)
        {
            DebugFunction();
        }
    }

    private void DebugFunction()
    {
        _debug = true;

        float min = 0;
        float max = 0;
        
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                if (_elevations[x, y] < min)
                {
                    min = _elevations[x, y];
                }
                else if (_elevations[x, y] > max)
                {
                    max = _elevations[x, y];
                }
                
                //Show each elements of _elevation
                //Debug.Log(_elevations[x,y]);
            }
        }
        
        //Show min & max of _elevations
        Debug.Log(min + " " + max);
    }
}
