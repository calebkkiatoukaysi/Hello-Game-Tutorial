using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HelloGameExample;

public class HelloGame : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Texture2D _texture;
    private Vector2 _position;
    private Vector2 _direction;

    private const float SpriteScale = 0.1f;

    public HelloGame
()
    {
        _graphics = new GraphicsDeviceManager(this);
        _graphics.PreferredBackBufferWidth = 1280;
        _graphics.PreferredBackBufferHeight = 720;
        _graphics.ApplyChanges();
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        MathHelper.Random random = new();
        _texture = Content.Load<Texture2D>("Anonymous-Flag-of-Kansas");

        // this ensures that the sprite is on screen, got a bug where it would spawn slightly outside, making it glitchy.
        float maxX = GraphicsDevice.Viewport.Width - _texture.Width * SpriteScale;
        float maxY = GraphicsDevice.Viewport.Height - _texture.Height * SpriteScale;


        _position = new Vector2(random.NextFloat() * maxX, random.NextFloat() * maxY);

        _direction = new Vector2(100 * random.NextFloat() - 150, 100 * random.NextFloat() - 150);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();
        _position += _direction * (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (_position.X < 0 || _position.X > GraphicsDevice.Viewport.Width - _texture.Width * SpriteScale)
        {
            _direction.X *= -1;
        }
        if (_position.Y < 0 || _position.Y > GraphicsDevice.Viewport.Height - _texture.Height * SpriteScale)
        {
            _direction.Y *= -1;
        }
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Lavender);

        _spriteBatch.Begin();
        _spriteBatch.Draw(_texture, _position, null, Color.White, 0f, Vector2.Zero, SpriteScale, SpriteEffects.None, 0f);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
