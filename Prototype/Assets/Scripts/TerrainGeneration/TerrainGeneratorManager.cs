using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TerrainGeneratorManager : MonoBehaviour
{
    [SerializeField] TerrainLayer residentialLayer;
    [SerializeField] Terrain target;

    [SerializeField] TextAsset mapConfig;

     void Start() {
        this.Regenerate();
    }

    void Update()
    {
        
    }

    private void Regenerate() {
        Zones zones = JsonUtility.FromJson<Zones>(mapConfig.text);
        List<Zone> residentialZones = searchResidentialZones(zones.zones);
        int textureResolution = target.terrainData.alphamapResolution;
        int mapWidth = target.terrainData.alphamapWidth;
        int mapHeight = target.terrainData.alphamapHeight;
        int layers = target.terrainData.alphamapLayers;
        Debug.Log("Resolution " + textureResolution);
        Debug.Log("Map Width " + mapWidth);
        Debug.Log("Map Height " + mapHeight);
        Debug.Log("Layers " + layers);

        float[,,] splatMap = new float[mapWidth, mapHeight, target.terrainData.alphamapLayers];
        for(int y = 0; y < mapHeight; y++) {
            for(int x = 0; x < mapWidth; x++) {
                bool shouldPaint = PaintIfResidential(x, y, residentialZones);
                splatMap[x, y, 0] = shouldPaint ? 0.0f : 1.0f;
                splatMap[x, y, 1] = shouldPaint ? 1.0f : 0.0f;
            }
        }
        target.terrainData.SetAlphamaps(0, 0, splatMap);
    }

    private bool PaintIfResidential(int x, int y, List<Zone> residentialZones) {
        return residentialZones.Any(zone => zone.Belongs(x, y));
    }

    private List<Zone> searchResidentialZones(List<Zone> zones) {
        return zones.FindAll( zone => zone.type == ZoneType.Residential);
    }

#if UNITY_EDITOR
    public void OnRegenerate() {
        this.Regenerate();
    }
#endif //UNITY_EDITOR
}

