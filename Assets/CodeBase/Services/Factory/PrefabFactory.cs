using CodeBase.Services.AssetManagment;
using CodeBase.Services.StaticData;
using CodeBase.StaticData.Strings;
using CodeBase.Tools;
using UnityEngine;
using Zenject;

namespace CodeBase.Services.Factory
{
    public class PrefabFactory : IPrefabFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IStaticDataService _staticDataService;
        private readonly DiContainer _container;
        private GameObject _uiRoot;

        public PrefabFactory(DiContainer container, IAssetProvider assetProvider, IStaticDataService staticDataService)
        {
            _assetProvider = assetProvider;
            _staticDataService = staticDataService;
            _container = container;
        }

        public GameObject CreatePlayer(Vector3 at) =>
            _assetProvider.Instantiate(PrefabsPath.Player, at)
                .With(Inject);

        public GameObject CreateUIRoot() =>
            _assetProvider.Instantiate(PrefabsPath.UI)
                .With(Inject)
                .With(_ => _uiRoot = _);

        public GameObject CreateAliveUI() =>
            _assetProvider.Instantiate(PrefabsPath.UIAlive, _uiRoot.transform, _uiRoot.transform)
                .With(Inject);

        public GameObject CreateDeadUI() =>
            _assetProvider.Instantiate(PrefabsPath.UIDead, _uiRoot.transform, _uiRoot.transform)
                .With(Inject);

        private void Inject(GameObject gameObject) =>
            _container.InjectGameObject(gameObject);
    }
}