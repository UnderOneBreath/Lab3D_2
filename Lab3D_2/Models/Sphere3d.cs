using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;

namespace Lab3D_2.Models
{
    public class Sphere3d : ModelVisual3D
    {
        private const int max_i = 60;
        private const int max_j = 60;
        private Point3D[,] position = new Point3D[max_i + 1, max_j];
        private Point[,] texture = new Point[max_i + 1, max_j];

        public BitmapImage earthImage = new BitmapImage(new Uri("earth2.bmp", UriKind.Relative));

        private DiffuseMaterial[] frontMaterial = new DiffuseMaterial[max_j - 1];

        public Sphere3d()
        {
            GenerateImageMaterials();
            GenerateSphere(max_i, max_j);
            GenerateAllCylinders();
        }

        private void GenerateSphere(int longitudes, int latitudes)
        {
            double latitudeArcusIncrement = Math.PI / (latitudes - 1);
            double longitudeArcusIncrement = 2.0 * Math.PI / longitudes;
            for (int lat = 0; lat < latitudes; lat++)
            {
                double latitudeArcus = lat * latitudeArcusIncrement;
                double radius = Math.Sin(latitudeArcus);
                double y = Math.Cos(latitudeArcus);
                double textureY = (double)lat / (latitudes - 1);
                for (int lon = 0; lon <= longitudes; lon++)
                {
                    double longitudeArcus = lon * longitudeArcusIncrement;
                    position[lon, lat].X = radius * Math.Cos(longitudeArcus);
                    position[lon, lat].Y = y;
                    position[lon, lat].Z = -radius * Math.Sin(longitudeArcus);
                    texture[lon, lat].X = (double)lon / longitudes;
                    texture[lon, lat].Y = textureY;
                }
            }
        }

        private void GenerateImageMaterials()
        {
            ImageBrush imageBrush;
            double flatThickness = 1.0 / (max_i - 1);
            double minus = (double)(max_i);
            for (int i = 0; i < max_i - 1; i++)
            {
                imageBrush = new ImageBrush(earthImage);
                imageBrush.Viewbox = new Rect(0, i * flatThickness, minus / max_i, flatThickness);
                frontMaterial[i] = new DiffuseMaterial(imageBrush);
            }
        }

        private void GenerateAllCylinders()
        {
            Model3DGroup model3DGroup = new Model3DGroup();
            for (int lat = 0; lat < max_j - 1; lat++)
            {
                GeometryModel3D geometryModel3D = new GeometryModel3D();
                geometryModel3D.Geometry = GenerateCylinder(lat);
                geometryModel3D.Material = frontMaterial[lat];
                model3DGroup.Children.Add(geometryModel3D);
            }
            Content = model3DGroup;
        }

        private MeshGeometry3D GenerateCylinder(int lat)
        {
            MeshGeometry3D meshGeometry3D = new MeshGeometry3D();
            for (int lon = 0; lon <= max_i; lon++)
            {
                Point3D p0 = position[lon, lat];
                Point3D p1 = position[lon, lat + 1];
                meshGeometry3D.Positions.Add(p0);
                meshGeometry3D.Positions.Add(p1);
                meshGeometry3D.Normals.Add((Vector3D)p0);
                meshGeometry3D.Normals.Add((Vector3D)p1);
                meshGeometry3D.TextureCoordinates.Add(texture[lon, lat]);
                meshGeometry3D.TextureCoordinates.Add(texture[lon, lat + 1]);
            }
            for (int lon = 1; lon < meshGeometry3D.Positions.Count - 2; lon += 2)
            {
                meshGeometry3D.TriangleIndices.Add(lon - 1);
                meshGeometry3D.TriangleIndices.Add(lon);
                meshGeometry3D.TriangleIndices.Add(lon + 1);
                meshGeometry3D.TriangleIndices.Add(lon + 1);
                meshGeometry3D.TriangleIndices.Add(lon);
                meshGeometry3D.TriangleIndices.Add(lon + 2);
            }
            return meshGeometry3D;
        }
    }
}
