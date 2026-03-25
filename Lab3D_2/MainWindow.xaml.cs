using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Media3D;
using Lab3D_2.Models;

namespace Lab3D_2
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            BuildCubeTab();
            BuildSphereTab();
        }

        private void BuildCubeTab()
        {
            Viewport3D viewport = new Viewport3D();

            viewport.Camera = new PerspectiveCamera
            {
                Position = new Point3D(1, 1, 2),
                LookDirection = new Vector3D(-2, -2, -3),
                FieldOfView = 60,
                UpDirection = new Vector3D(0, 1, 0)
            };

            ModelVisual3D light1 = new ModelVisual3D();
            light1.Content = new DirectionalLight(Colors.White, new Vector3D(-1, -1, -1));
            viewport.Children.Add(light1);

            ModelVisual3D light2 = new ModelVisual3D();
            light2.Content = new DirectionalLight(Color.FromRgb(0x66, 0x66, 0x66), new Vector3D(1, 1, 1));
            viewport.Children.Add(light2);

            Cube3D cube = new Cube3D();

            AxisAngleRotation3D rotX = new AxisAngleRotation3D(new Vector3D(1, 0, 0), 0);
            AxisAngleRotation3D rotY = new AxisAngleRotation3D(new Vector3D(0, 1, 0), 0);
            AxisAngleRotation3D rotZ = new AxisAngleRotation3D(new Vector3D(0, 0, 1), 0);

            Transform3DGroup transformGroup = new Transform3DGroup();
            transformGroup.Children.Add(new RotateTransform3D(rotX));
            transformGroup.Children.Add(new RotateTransform3D(rotY));
            transformGroup.Children.Add(new RotateTransform3D(rotZ));
            cube.Transform = transformGroup;

            viewport.Children.Add(cube);
            gridCube.Children.Add(viewport);

            Storyboard sb = new Storyboard();

            DoubleAnimation animX = new DoubleAnimation(0, 0, new Duration(TimeSpan.FromSeconds(5)));
            Storyboard.SetTarget(animX, cube);
            Storyboard.SetTargetProperty(animX,
                new PropertyPath("(ModelVisual3D.Transform).(Transform3DGroup.Children)[0].(RotateTransform3D.Rotation).(AxisAngleRotation3D.Angle)"));
            sb.Children.Add(animX);

            DoubleAnimation animY = new DoubleAnimation(0, 2720, new Duration(TimeSpan.FromSeconds(25)));
            Storyboard.SetTarget(animY, cube);
            Storyboard.SetTargetProperty(animY,
                new PropertyPath("(ModelVisual3D.Transform).(Transform3DGroup.Children)[1].(RotateTransform3D.Rotation).(AxisAngleRotation3D.Angle)"));
            sb.Children.Add(animY);

            DoubleAnimation animZ = new DoubleAnimation(0, 0, new Duration(TimeSpan.FromSeconds(5)));
            Storyboard.SetTarget(animZ, cube);
            Storyboard.SetTargetProperty(animZ,
                new PropertyPath("(ModelVisual3D.Transform).(Transform3DGroup.Children)[2].(RotateTransform3D.Rotation).(AxisAngleRotation3D.Angle)"));
            sb.Children.Add(animZ);

            gridCube.MouseDown += (s, args) => { sb.Begin(this); };
        }

        private void BuildSphereTab()
        {
            Viewport3D viewport = new Viewport3D();

            viewport.Camera = new PerspectiveCamera
            {
                Position = new Point3D(2, 2, 3),
                LookDirection = new Vector3D(-2, -2, -3),
                FieldOfView = 60,
                UpDirection = new Vector3D(0, 1, 0)
            };

            ModelVisual3D light1 = new ModelVisual3D();
            light1.Content = new DirectionalLight(Colors.White, new Vector3D(-1, -1, -1));
            viewport.Children.Add(light1);

            ModelVisual3D light2 = new ModelVisual3D();
            light2.Content = new DirectionalLight(Color.FromRgb(0x66, 0x66, 0x66), new Vector3D(1, 1, 1));
            viewport.Children.Add(light2);

            Sphere3d sphere = new Sphere3d();

            AxisAngleRotation3D rotX = new AxisAngleRotation3D(new Vector3D(1, 0, 0), 0);
            AxisAngleRotation3D rotY = new AxisAngleRotation3D(new Vector3D(0, 1, 0), 0);
            AxisAngleRotation3D rotZ = new AxisAngleRotation3D(new Vector3D(0, 0, 1), 0);

            Transform3DGroup transformGroup = new Transform3DGroup();
            transformGroup.Children.Add(new RotateTransform3D(rotX));
            transformGroup.Children.Add(new RotateTransform3D(rotY));
            transformGroup.Children.Add(new RotateTransform3D(rotZ));
            sphere.Transform = transformGroup;

            viewport.Children.Add(sphere);
            gridSphere.Children.Add(viewport);

            Storyboard sb = new Storyboard();

            DoubleAnimation animX = new DoubleAnimation(0, 0, new Duration(TimeSpan.FromSeconds(5)));
            Storyboard.SetTarget(animX, sphere);
            Storyboard.SetTargetProperty(animX,
                new PropertyPath("(ModelVisual3D.Transform).(Transform3DGroup.Children)[0].(RotateTransform3D.Rotation).(AxisAngleRotation3D.Angle)"));
            sb.Children.Add(animX);

            DoubleAnimation animY = new DoubleAnimation(0, 2720, new Duration(TimeSpan.FromSeconds(25)));
            Storyboard.SetTarget(animY, sphere);
            Storyboard.SetTargetProperty(animY,
                new PropertyPath("(ModelVisual3D.Transform).(Transform3DGroup.Children)[1].(RotateTransform3D.Rotation).(AxisAngleRotation3D.Angle)"));
            sb.Children.Add(animY);

            DoubleAnimation animZ = new DoubleAnimation(0, 0, new Duration(TimeSpan.FromSeconds(5)));
            Storyboard.SetTarget(animZ, sphere);
            Storyboard.SetTargetProperty(animZ,
                new PropertyPath("(ModelVisual3D.Transform).(Transform3DGroup.Children)[2].(RotateTransform3D.Rotation).(AxisAngleRotation3D.Angle)"));
            sb.Children.Add(animZ);

            gridSphere.MouseDown += (s, args) => { sb.Begin(this); };
        }
    }
}
