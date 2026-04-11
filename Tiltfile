# ── Infrastructure ────────────────────────────────────────────────────────────
docker_compose('./docker-compose.yml')

dc_resource('db',       labels=['infrastructure'])
dc_resource('redis',    labels=['infrastructure'])
dc_resource('azurite',  labels=['infrastructure'])
dc_resource('adminer',  labels=['infrastructure'],
    links=[link('http://localhost:8080', 'Adminer')])
dc_resource('redis-ui', labels=['infrastructure'],
    links=[link('http://localhost:8082', 'Redis Commander')])

# ── API — dotnet watch for hot-reload; runs on http://localhost:5235 ──────────
local_resource(
    'api',
    serve_cmd='dotnet watch run --project src/API/API.csproj --launch-profile http',
    serve_dir='apps/cocreateApi',
    resource_deps=['redis', 'azurite'],
    labels=['app'],
    links=[
        link('http://localhost:5235',          'API'),
        link('http://localhost:5235/scalar/v1', 'API Docs (Scalar)'),
    ],
)

# ── UI — Expo web dev server; needs to know where the API lives ───────────────
local_resource(
    'ui',
    serve_cmd='npx expo start',
    serve_dir='apps/cocreateUi',
    env={'EXPO_PUBLIC_API_URL': 'http://localhost:5235'},
    resource_deps=['api'],
    labels=['app'],
    links=[link('http://localhost:8081', 'UI (Web)')],
)

# ── Android Emulator — Medium Phone ──────────────────────────────────────────
local_resource(
    'android-emulator',
    serve_cmd='C:/Users/abith/AppData/Local/Android/Sdk/emulator/emulator.exe -avd Medium_Phone',
    labels=['app'],
)
