  /// <summary>
    /// Логика взаимодействия для DropFileControl.xaml
    /// </summary>
    public partial class DropFileControl : UserControl
    {
        public DropFileControl()
        {
            InitializeComponent();

        }
        static FrameworkPropertyMetadata propertymetadata =
new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault);




        public override void EndInit()
        {
            base.EndInit();
            lb.Content = NoFileLabel;

        }

        public string NoFileLabel
        {
            get { return (string)GetValue(NoFileLabelProperty); }
            set { SetValue(NoFileLabelProperty, value); }
        }

        // Using a DependencyProperty as the backing store for NoFileLabel.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NoFileLabelProperty =
            DependencyProperty.Register("NoFileLabel", typeof(string), typeof(DropFileControl), propertymetadata);



        public string FileName
        {
            get { return (string)GetValue(FileNameProperty); }
            set { SetValue(FileNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FileName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FileNameProperty =
            DependencyProperty.Register("FileName", typeof(string), typeof(DropFileControl), new PropertyMetadata(string.Empty));



        public DragDropEffects Effects
        {
            get { return (DragDropEffects)GetValue(EffectsProperty); }
            set { SetValue(EffectsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Effects.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EffectsProperty =
            DependencyProperty.Register("Effects", typeof(DragDropEffects), typeof(DropFileControl), new FrameworkPropertyMetadata(DragDropEffects.Copy, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));




        private void UIElement_OnDrop(object sender, DragEventArgs e)
        {
            var b = e.Data.GetDataPresent(DataFormats.FileDrop);

            if (b)
            {


                var sarr = e.Data.GetData(DataFormats.FileDrop);
                var a = sarr as string[];
                var fn = a.First();
                FileName = fn;
                Effects = GetEffects(e);
                lb.Content = Path.GetFileNameWithoutExtension(fn);
            }
        }

        private void UIElement_OnDragOver(object sender, DragEventArgs e)
        {

            var eff = GetEffects(e);
            e.Effects = eff;
        }

        private DragDropEffects GetEffects(DragEventArgs e)
        {
            var b = e.Data.GetDataPresent(DataFormats.FileDrop);
                var isCopy = ((e.KeyStates & DragDropKeyStates.ControlKey) == DragDropKeyStates.ControlKey);
            var effects = isCopy ? DragDropEffects.Copy : DragDropEffects.Move;
          return b ? effects : DragDropEffects.None;

        }
    }
