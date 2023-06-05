using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;


public class PoiScript : MonoBehaviour
{
    public double longitude;
    public double latitude;
    public string description;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject o = GameObject.Find("Descripcion");
            
            o.GetComponent<TMPro.TextMeshProUGUI>().text = description;
            UnityEngine.Debug.Log("Clicked");
        }
    }

    public void MapLocation()
    {

            double x = Math.Floor(MapManager.TileX);
            double y = Math.Floor(MapManager.TileY);
            int zoom = MapManager.zoom;

            double a = DrawCubeX(longitude, TileToWorldPos(x, y, zoom).X, TileToWorldPos(x + 1, y, zoom).X);
            double b = DrawCubeY(latitude, TileToWorldPos(x, y + 1, zoom).Y, TileToWorldPos(x, y, zoom).Y);

            //Debug.Log(" Pos" + a + "/" + b);
            this.transform.position = new Vector3((float)a - 0.5f, (float)b - 0.5f, 0.0f);

        UnityEngine.Debug.Log(longitude);
        UnityEngine.Debug.Log(latitude);


    }
    public struct Point
    {
        public double X;
        public double Y;
    }


    // p.X -> longitud
    // p.Y -> latitud
    // left upper corner
    public Point TileToWorldPos(double tile_x, double tile_y, int zoom)
    {
        Point p = new Point();
        double n = System.Math.PI - ((2.0 * System.Math.PI * tile_y) / System.Math.Pow(2.0, zoom));

        p.X = ((tile_x / System.Math.Pow(2.0, zoom) * 360.0) - 180.0);
        p.Y = (180.0 / System.Math.PI * System.Math.Atan(System.Math.Sinh(n)));

        return p;
    }

    public double DrawCubeY(double targetLat, double minLat, double maxLat)
    {
        double pixelY = ((targetLat - minLat) / (maxLat - minLat));
        return pixelY;
    }

    public double DrawCubeX(double targetLong, double minLong, double maxLong)
    {
        double pixelX = ((targetLong - minLong) / (maxLong - minLong));
        return pixelX;
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
