// <copyright file="BasePersonRoleConfiguration{TEntity}.cs" company="Филипченко Марина Алексеевна">
// Copyright (c) Филипченко Марина Алексеевна 2026. Library.
// </copyright>

namespace DataAccessLayer.Configurations.Abstractions
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Domain;
    using Domain.Abstract;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    /// Базовая конфигурация для ролей персоны (автор, редактор, переводчик, рецензент).
    /// </summary>
    /// <typeparam name="TEntity"> Целевой тип сущности. </typeparam>
    internal abstract class BasePersonRoleConfiguration<TEntity> : BaseEntityConfiguration<TEntity>
        where TEntity : PersonRole<TEntity>
    {
        private readonly Expression<Func<Person, TEntity?>> personExpression;

        private readonly Expression<Func<TEntity, IEnumerable<Manuscript>?>>? manuscriptsExpression;

        private readonly Expression<Func<Manuscript, IEnumerable<TEntity>?>>? personsExpression;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="BasePersonRoleConfiguration{TEntity}"/>.
        /// </summary>
        /// <param name="personExpression"> Связь Персоны и Сущности. </param>
        /// <param name="manuscriptsExpression"> Связь целевой сущности и Рукописей . </param>
        /// <param name="personsExpression"> Связь Рукописи и Сущностей (у одной рукописи несколько авторов). </param>
        /// <param name="tableName"> Название таблицы. </param>
        /// <param name="tableComment"> Комментарий к таблице. </param>
        protected BasePersonRoleConfiguration(
            Expression<Func<Person, TEntity?>> personExpression,
            Expression<Func<TEntity, IEnumerable<Manuscript>?>>? manuscriptsExpression = null,
            Expression<Func<Manuscript, IEnumerable<TEntity>?>>? personsExpression = null,
            string? tableName = null,
            string? tableComment = null)
            : base(tableName, tableComment)
        {
            this.personExpression = personExpression;
            this.manuscriptsExpression = manuscriptsExpression;
            this.personsExpression = personsExpression;
        }

        /// <inheritdoc/>
        public override void Configure(EntityTypeBuilder<TEntity> builder)
        {
            base.Configure(builder);

            _ = builder.HasOne(translator => translator.Person)
                .WithOne(this.personExpression)
                .HasForeignKey<TEntity>(author => author.PersonId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            if (this.manuscriptsExpression is not null
                && this.personsExpression is not null)
            {
                _ = builder.HasMany(this.manuscriptsExpression)
                    .WithMany(this.personsExpression);
            }

            _ = builder.HasIndex(entity => entity.PersonId)
                .IsUnique();
        }
    }
}
