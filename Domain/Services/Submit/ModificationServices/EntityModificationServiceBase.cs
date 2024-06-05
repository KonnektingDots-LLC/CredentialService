using AutoMapper;
using cred_system_back_end_app.Application.Common.EqualityComparers;
using cred_system_back_end_app.Domain.Common;
using cred_system_back_end_app.Infrastructure.Data.ContextEntity;

namespace cred_system_back_end_app.Domain.Services.Submit.ModificationServices
{
    public abstract class EntityModificationServiceBase
    {
        private readonly DbContextEntity _dbContext;
        private readonly IMapper _mapper;

        public EntityModificationServiceBase(
            DbContextEntity dbContextEntity,
            IMapper mapper
        )
        {
            _dbContext = dbContextEntity;
            _mapper = mapper;
        }

        protected virtual async Task UpdateListMember<TListMemberEntity>(TListMemberEntity newEntity, IEnumerable<TListMemberEntity> oldEntities)
            where TListMemberEntity : ListMemberEntityBase
        {
            var oldEntity = oldEntities.Single(o => o.PublicId == newEntity.PublicId);

            _mapper.Map(newEntity, oldEntity);
        }

        protected virtual async Task UpdateListMember<TListMemberEntity>(TListMemberEntity newEntity, TListMemberEntity oldEntity)
             where TListMemberEntity : ListMemberEntityBase
        {
            _mapper.Map(newEntity, oldEntity);
        }

        protected virtual async Task AddListMember<TListMemberEntity>(TListMemberEntity newEntity)
            where TListMemberEntity : ListMemberEntityBase
        {
            _dbContext.Add(newEntity);
        }

        protected virtual async Task AddAnotherListMember<TListMemberEntity>(TListMemberEntity newEntity)
        {
            _dbContext.Add(newEntity);
        }

        protected virtual async Task RemoveListMembers<TListMemberEntity>(IEnumerable<TListMemberEntity> oldEntities)
            where TListMemberEntity : ListMemberEntityBase
        {
            _dbContext.RemoveRange(oldEntities);
        }

        protected async Task ModifyList<TListMemberEntity>(
            IEnumerable<TListMemberEntity> oldCollection,
            IEnumerable<TListMemberEntity> newCollection,
            Func<TListMemberEntity, Task> updateAction,
            Func<TListMemberEntity, Task> insertAction,
            Func<IEnumerable<TListMemberEntity>, Task> deleteAction
        ) where TListMemberEntity : ListMemberEntityBase
        {
            var publicIdComparer = new ListMemberEntityComparer<TListMemberEntity>();

            var entitiesToUpdate = newCollection.Intersect(oldCollection, publicIdComparer);
            var entitiesToInsert = newCollection.Except(entitiesToUpdate, publicIdComparer);
            var entitiesToDelete = oldCollection.Except(entitiesToUpdate, publicIdComparer);

            foreach (var submittedEntity in entitiesToUpdate)
            {
                await updateAction(submittedEntity);
            }

            foreach (var newEntity in entitiesToInsert)
            {
                await insertAction(newEntity);
            }

            if (entitiesToDelete.Any())
            {
                await deleteAction(entitiesToDelete);
            }
        }

        protected async Task ModifyEntity<T>(T newEntity, T? oldEntity)
        {
            if (newEntity == null && oldEntity != null)
            {
                _dbContext.Remove(oldEntity);
            }
            else if (oldEntity == null && newEntity != null)
            {
                _dbContext.AddRange(newEntity);
            }
            else
            {
                _mapper.Map(newEntity, oldEntity);
            }
        }

        protected async Task ModifyRelations<T>(IEnumerable<T> newRelationRecords, IEnumerable<T> oldRelationRecords, IEqualityComparer<T> equalityComparer)
        {
            var relationsToInsert = newRelationRecords.Except(oldRelationRecords, equalityComparer);
            var relationsToDelete = oldRelationRecords.Except(newRelationRecords, equalityComparer);

            foreach (var rel in relationsToInsert)
            {
                _dbContext.Add(rel);
            }

            foreach (var rel in relationsToDelete)
            {
                _dbContext.Remove(rel);
            }
        }

        protected async Task AddRelations<T>(IEnumerable<T> newRelationRecords)
        {

            foreach (var rel in newRelationRecords)
            {
                _dbContext.Add(rel);
            }
        }
    }
}
