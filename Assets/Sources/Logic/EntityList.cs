using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class EntityList : MonoBehaviour
    {
        [SerializeField] private Entity _startEntity;

        [SerializeField] private List<Entity> _entities = new();

        private void Awake()
        {
            _entities.Add(_startEntity);
        }

        private void OnEnable()
        {
            _startEntity.CreatedNewEntity += OnCreated;
            _startEntity.Destroyed += OnDestroyed;
        }

        private void OnDisable()
        {
            foreach (var entity in _entities)
            {
                if (entity != null)
                {
                    entity.CreatedNewEntity -= OnCreated;
                    entity.Destroyed -= OnDestroyed;
                }
            }
        }

        private void OnDestroyed()
        {
            for (int i = 0; i < _entities.Count; i++)
            {
                if (_entities[i] != null)
                    _entities[i].DetouchAll();
            }
        }

        private void OnCreated(Entity entity)
        {
            _entities.Add(entity);
            entity.CreatedNewEntity += OnCreated;
            entity.Destroyed += OnDestroyed;
        }
    }
}