// <copyright file="BaseRepository.cs" company="Васильева Марина Алексеевна">
// Copyright (c) Васильева Марина Алексеевна 2024. Library.
// </copyright>

namespace Repository
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using DataAccessLayer;
    using Domain;

    /// <summary>
    /// Базовый класс репозиториев.
    /// </summary>
    /// <typeparam name="TEntity"> Целевой тип сущности. </typeparam>
    public abstract class BaseRepository<TEntity>
        where TEntity : class, IEntity<TEntity>
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="BaseRepository{TEntity}"/>.
        /// </summary>
        /// <param name="dataContext">Контекст доступа к данным.</param>
        protected BaseRepository(DataContext dataContext)
        {
            this.DataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
        }

        /// <summary>
        /// Контекст доступа к данным.
        /// </summary>
        public DataContext DataContext { get; }

        /// <summary>
        /// Создает полку.
        /// </summary>
        /// <param name="entity">Сущность.</param>
        /// <param name="saveNow">Надо ли сохранять сущность после изменения. </param>
        /// <returns>Контекст доступа к сущности.</returns>
        public TEntity Create(TEntity entity, bool saveNow = true)
        {
            var result = this.DataContext.Add(entity).Entity;
            _ = this.Save(saveNow);
            return result;
        }

        /// <summary>
        /// Удаляет полку.
        /// </summary>
        /// <param name="entity">Сущность.</param>
        /// <param name="saveNow">Надо ли сохранять сущность после изменения. </param>
        /// <returns>Измененный контекст доступа к сущности.</returns>
        public TEntity Delete(TEntity entity, bool saveNow = true)
        {
            var result = this.DataContext.Remove(entity).Entity;
            _ = this.Save(saveNow);
            return result;
        }

        /// <summary>
        /// Поиск множества сущностей по предикату (<paramref name="predicate"/>).
        /// </summary>
        /// <param name="predicate">Предикат, которому должна удовлетворять сушность.</param>
        /// <returns>Множество (<see cref="IQueryable{TEntity}"/>) всех сущностей.</returns>
        public IQueryable<TEntity> Filter(Expression<Func<TEntity, bool>> predicate) => this.GetAll().Where(predicate);

        /// <summary>
        /// Поиск полки по предикату (<paramref name="predicate"/>).
        /// </summary>
        /// <param name="predicate">Предикат, которому должна удовлетворять сущность.</param>
        /// <returns>Сущность или <see langword="null"/>.</returns>
        public TEntity? Find(Expression<Func<TEntity, bool>> predicate) => this.GetAll().FirstOrDefault(predicate);

        /// <summary>
        /// Получение конкретной сущности по её идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор сущности.</param>
        /// <returns>Сушность.</returns>
        public TEntity? Get(Guid id) => this.GetAll().SingleOrDefault(entity => entity.Id == id);

        /// <summary>
        /// Получение всех сущностей.
        /// </summary>
        /// <returns> Множество (<see cref="IQueryable{TEntity}"/>) всех сущностей.</returns>
        public abstract IQueryable<TEntity> GetAll();

        /// <summary>
        /// Изменяет Сущность.
        /// </summary>
        /// <param name="entity">Сушность.</param>
        /// <param name="saveNow">Надо ли сохранять сущность после изменения. </param>
        /// <returns>Измененный контекст доступа к сущности.</returns>
        public TEntity Update(TEntity entity, bool saveNow = true)
        {
            var result = this.DataContext.Update(entity).Entity;
            _ = this.Save(saveNow);
            return result;
        }

        /// <summary>
        /// Сохраняет контекст в БД.
        /// </summary>
        /// <param name="saveNow">Надо ли сохранять сущность после изменения. </param>
        /// <returns>Количество измененных сущностей.</returns>
        private int Save(bool saveNow = true)
        {
            return saveNow
                ? this.DataContext.SaveChanges()
                : 0;
        }
    }
}
