using System.IO;
using Newtonsoft.Json;
using PicoSystem.Editor.Building;
using PicoSystem.Editor.Helpers;
using PicoSystem.Framework;
using PicoSystem.Framework.Common;
using PicoSystem.Framework.Content;
using PicoSystem.Framework.Graphics;
using PicoSystem.Framework.Input.Keyboard;
using PicoSystem.Framework.Scripting;

namespace PicoSystem.Editor
{
    /*public enum RunningMode
    {
        Editor,
        Game,
        Error
    }

    public class PicoEditor : PicoGame
    {
        public static readonly string Version = "0.0.1";

        public GameProject GameProject { get; private set; }

        public RunningMode RunningMode { get; private set; }

        private readonly GameBuilder _gameBuilder;

        private string _currentMsg;

        private readonly Rect _editorMsgArea = Rect.FromBox(50, 400, 700, 200);
        private readonly Rect _errorScreenMsgArea;

        private PicoScript _gameScript;

        private bool _gameReady;

        private float _currentMsgDuration = 3.0f;

        private float _msgTimer;

        private Color _bgColor = Color.Blue;
        private Color _textColor = Color.White;

        public PicoEditor()
        {
            DisplaySize = new Size(800, 600);

            Title = $"PicoEditor :: {Version}";

            _gameBuilder = new GameBuilder();

            _errorScreenMsgArea = Rect.FromBox(20, 70, DisplaySize.Width - 20, DisplaySize.Height - 20);

            RunningMode = RunningMode.Editor;
        }

        public override void Load()
        {
        }

        public override void OnTerminate()
        {
            base.OnTerminate();

            if (GameProject != null)
            {
                _gameBuilder.Cleanup(GameProject);
            }
        }

        public override void Update(float dt)
        {
            if (Input.KeyPressed(Key.R))
            {
                BuildAndRunGame();
            }

            if (RunningMode == RunningMode.Editor)
            {
                if (Input.KeyPressed(Key.N))
                {
                    NewProject();
                }

                else if (Input.KeyPressed(Key.L))
                {
                    LoadProject();
                }
                

                else if (Input.KeyReleased(Key.Q))
                {
                    Quit();
                }

              
                else if (Input.KeyPressed(Key.E))
                {
                    ExportGame();
                }

                
            }
            else if (RunningMode == RunningMode.Game || RunningMode == RunningMode.Error)
            {

                if (Input.KeyPressed(Key.Escape))
                {
                    RunningMode = RunningMode.Editor;
                    return;
                }

                if (RunningMode == RunningMode.Game && _gameReady)
                {
                    _gameScript?.Update(dt);
                }
            }

            if (_currentMsg != null && _currentMsgDuration > 0.0f)
            {
                _msgTimer += 0.016f;

                if (_msgTimer >= _currentMsgDuration)
                {
                    _currentMsg = null;

                    _msgTimer = 0.0f;
                }
            }

        }

        public override void Draw(PicoGfx gfx)
        {
            if (RunningMode == RunningMode.Editor)
            {

                gfx.SetColor(ref _bgColor);
                gfx.FillRect(10, 10, DisplaySize.Width - 20, DisplaySize.Height - 20);

                if (GameProject != null)
                {
                    //renderer.DrawText($"Current Game Project: {GameProject.Name}", 50, 100, Color.YellowGreen, 2);
                }

                //renderer.DrawText("Press 'N' to Create a new Game Project", 50, 150, Color.White, 2);

                //renderer.DrawText("Press 'L' to Load a Game Project", 50, 200, Color.White, 2);

                //renderer.DrawText("Press 'R' Run Game", 50, 250, Color.White, 2);

                //renderer.DrawText("Press 'E' to Export Game", 50, 300, Color.White, 2);

                //renderer.DrawText("Press 'Q' to Quit", 50, 350, Color.White, 2);

                if (_currentMsg != null)
                {
                    //renderer.DrawTextArea(_currentMsg, _editorMsgArea, Color.Red, 1);
                }
            }
            else if (RunningMode == RunningMode.Game && _gameReady)
            {
                _gameScript?.Draw(gfx);
            }
            else if (RunningMode == RunningMode.Error)
            {
                //renderer.DrawTextArea(_currentMsg, _errorScreenMsgArea, Color.Red, 2);
            }
        }

        private void NewProject()
        {
            string directory = TinyFD.SelectFolderDialog("Select Target Directory", null);

            if (directory != null)
            {
                string generatedProjectFilePath = GameGenerator.Generate(directory, "NewGame");

                LoadProjectFromProjectFile(generatedProjectFilePath);
            }

            
        }

        private void LoadProject()
        {
            string projectFilePath = TinyFD.OpenFileDialog("Select Game Project File:", null, 1, new[] {"*.json"},
                "Game Project File (*.json)", allowMultipleSelects: false);

            if (!string.IsNullOrEmpty(projectFilePath))
            {
                LoadProjectFromProjectFile(projectFilePath);
            }

            _gameBuilder.Cleanup(GameProject);

        }

        private void LoadProjectFromProjectFile(string projectFilePath)
        {
            string projectFileText = File.ReadAllText(projectFilePath);

            GameProjectDefinition definition = JsonConvert.DeserializeObject<GameProjectDefinition>(projectFileText);

            string projectDir = Path.GetDirectoryName(projectFilePath);

            GameProject = new GameProject(projectDir, definition);

            PostScreenMsg($"Opened game project at: {projectDir}", 5f);
        }

        private async void BuildAndRunGame()
        {
            if (GameProject == null)
            {
                PostScreenMsg("No Game Project Loaded!", 5f);
                return;
            }

            _gameReady = false;

            PostScreenMsg("Running Game...", 5f);

            BuildResult result = await _gameBuilder.Build(GameProject, releaseMode: false);

            if (result.Ok)
            {
                if (result.ScriptData != null)
                {
                    _gameScript = ScriptLoader.Load(this, result.ScriptData);
                }

                if (_gameScript != null)
                {
                    if (result.AssetsInvalidated)
                    {
                    }

                    _gameScript.Init();

                    RunningMode = RunningMode.Game;

                    _gameReady = true;
                }
                else
                {
                    PostScreenMsg("Lost Game Script", 5f);
                }

            }
            else
            {
                RunningMode = RunningMode.Error;

                PostScreenMsg(result.ResultMessage);
            }
        }

        private async void ExportGame()
        {
            if (GameProject == null)
            {
                PostScreenMsg("No Game Project Loaded!", 5f);
                return;
            }

            PostScreenMsg("Exporting Game...", 5f);

            BuildResult result = await _gameBuilder.Export(GameProject);

            if (result.Ok)
            {
                PostScreenMsg("Game Exported Successfuly", 5f);
            }
            else
            {
                PostScreenMsg(result.ResultMessage, 30f);
            }
        }

        private void PostScreenMsg(string msg, float duration=0.0f)
        {
            _currentMsg = msg;
            _currentMsgDuration = duration;
            _msgTimer = 0.0f;
        }
    }*/
}
