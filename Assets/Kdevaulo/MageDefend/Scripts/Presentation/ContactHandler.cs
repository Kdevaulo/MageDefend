using System.Collections.Generic;
using System.Linq;

using Kdevaulo.MageDefend.Model;

using UnityEngine;

namespace Kdevaulo.MageDefend.Presentation
{
    public class ContactHandler
    {
        private List<Enemy> _activeEnemies;
        private UnitModel _playerModel;

        public ContactHandler(UnitModel playerModel)
        {
            _playerModel = playerModel;
        }

        public void Initialize(List<Enemy> activeEnemies)
        {
            _activeEnemies = activeEnemies;
        }

        public void HandleContact(EnemyView enemyView, GameObject target)
        {
            if (target.TryGetComponent<SpellView>(out var spellView))
            {
                HitEnemy(enemyView, spellView);
            }

            if (target.TryGetComponent<PlayerView>(out var playerView))
            {
                HitPlayer(playerView, enemyView);
            }
        }

        private void HitEnemy(EnemyView enemyView, SpellView spellView)
        {
            var enemyItem = _activeEnemies.FirstOrDefault(x => x.EnemyView == enemyView);
            var spellModel = SpellController.LaunchedSpells.FirstOrDefault(x => x.Value == spellView).Key;

            if (enemyItem == null)
            {
                Object.Destroy(enemyView.gameObject);
                return;
            }

            if (spellModel == null)
            {
                Debug.Log("REMOVED SPELL");
                Object.Destroy(spellView.gameObject);
                return;
            }

            var damage = spellModel.Damage;
            var enemyModel = enemyItem.EnemyModel;
            enemyModel.TakeDamage(damage);

            if (enemyModel.Hp.Value <= 0)
            {
                Object.Destroy(enemyItem.EnemyView.gameObject);
                _activeEnemies.Remove(enemyItem);
            }
        }

        private void HitPlayer(PlayerView playerView, EnemyView enemyView)
        {
            var enemyItem = _activeEnemies.FirstOrDefault(x => x.EnemyView == enemyView);

            if (enemyItem == null)
            {
                Object.Destroy(enemyView.gameObject);
                return;
            }

            var damage = enemyItem.EnemyModel.Damage.Value;
            _playerModel.TakeDamage(damage);

            if (_playerModel.Hp.Value <= 0)
            {
                Debug.Log("Game Over");
            }
        }
    }
}