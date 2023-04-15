using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainController : MonoBehaviour
{
    //singleton
    public static TerrainController Instance;

    public Terrain terrain;
    private float scrollX = 0;
    public float scrollSpeedX = 0;
    private float scrollY = 0;
    public float scrollSpeedY = 0;
    public int terrainSize = 200;

    [Range(0,.1f)] public float frequencyX = .1f;
    [Range(0,.1f)] public float frequencyY = .1f;
    public float amplitude = .1f;
    public float amplitudeIncrementSpeed = 0;
    public float amplitudeTarget = 0;

    int res;
    float[,] mesh;


    void Start()
    {
        Instance = this;
        this.terrain.terrainData.heightmapResolution = terrainSize;
        res = this.terrain.terrainData.heightmapResolution;
        mesh = new float[res, res];
        mesh = this.terrain.terrainData.GetHeights(0,0,res,res);
    }

    void UpdateTerrain(){
        scrollX += scrollSpeedX;
        scrollY += scrollSpeedY;

        if(amplitude < amplitudeTarget) {
            amplitude += amplitudeIncrementSpeed;
        } else if (amplitude > amplitudeTarget){
            amplitude -= amplitudeIncrementSpeed;
        }

        for(int x = 0; x < res; x++){
            for (int y = 0; y < res; y++){
                mesh[x,y] = Mathf.PerlinNoise((x+scrollX)*frequencyX, (y+scrollY)*frequencyY)*amplitude;
            }
        }
        this.terrain.terrainData.SetHeights(0,0,mesh);
    }

    public void SetAmplitudeTargetAndSpeed(float target, float speed)
    {
        amplitudeTarget = target;
        amplitudeIncrementSpeed = speed;
    }

    public void SetScrollSpeed(float x, float y){
        scrollSpeedX = x;
        scrollSpeedY = y;
    }

    void Update()
    {
        UpdateTerrain();
    }
}
