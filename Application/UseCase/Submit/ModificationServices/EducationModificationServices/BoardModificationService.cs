using AutoMapper;
using cred_system_back_end_app.Application.Common.EqualityComparers;
using cred_system_back_end_app.Application.Common.Mappers.DTOToEntity;
using cred_system_back_end_app.Application.CRUD.Document;
using cred_system_back_end_app.Application.UseCase.Submit.DTO.EducationDTOs;
using cred_system_back_end_app.Infrastructure.DB.ContextEntity;
using cred_system_back_end_app.Infrastructure.DB.Entity;
using Microsoft.EntityFrameworkCore;

namespace cred_system_back_end_app.Application.UseCase.Submit.ResubmitServices.EducationResubmitServices
{
    public class BoardModificationService : EntityModificationServiceBase
    {
        private readonly DbContextEntity _dbContextEntity;
        private readonly BoardSpecialtyComparer _boardSpecialtyComparer;
        private readonly DocumentCase _documentCase;

        public BoardModificationService
        (
            DbContextEntity dbContextEntity,
            IMapper mapper,
            BoardSpecialtyComparer boardSpecialtyComparer,
            DocumentCase documentCase
        ) : base(dbContextEntity, mapper)
        {
            _dbContextEntity = dbContextEntity;
            _boardSpecialtyComparer = boardSpecialtyComparer;
            _documentCase = documentCase;
        }

        public async Task Modify(int providerId, IEnumerable<BoardCertificateDTO> boardCertificateDTOs) 
        {
            var newSpecialties = GetPublicIdToSubSpecialtiesMap(boardCertificateDTOs);

            foreach (var boardDTO in boardCertificateDTOs)
            {
                var documentLocationBoardCertificates = _documentCase.GetDocumentLocationEntityByProviderIdDocTypeFilename(
                                            providerId,
                                            boardDTO.CertificateFile.DocumentTypeId,
                                            boardDTO.CertificateFile.Name);

                boardDTO.CertificateFile.AzureBlobFilename = documentLocationBoardCertificates?.AzureBlobFilename;
            }


            var newBoardEntities = boardCertificateDTOs.Select(b => Education.GetBoardEntity(b, providerId));

            var currentBoardEntities = await _dbContextEntity.Board
                .Where(b => b.ProviderId == providerId)
                .Include(b => b.BoardDocument)
                .ToListAsync();

            var currentSpecialties = await GetPrivateIdToSpecialtiesMap(currentBoardEntities);

            await ModifyList
            (
                currentBoardEntities,
                newBoardEntities,
                newBoard => UpdateBoard(newBoard, currentBoardEntities, newSpecialties, currentSpecialties),
                newBoard => AddBoard(newBoard, newSpecialties),
                boardsToDelete => RemoveBoard(boardsToDelete, currentSpecialties)
            );
        }

        private IDictionary<string, int[]> GetPublicIdToSubSpecialtiesMap(IEnumerable<BoardCertificateDTO> boardCertificateDTOs)
        {
            var dictionary = new Dictionary<string, int[]>();

            foreach (var boardDTO in boardCertificateDTOs)
            {
                dictionary.Add(boardDTO.PublicId, boardDTO.SpecialtyBoard);
            }

            return dictionary;
        }

        private async Task UpdateBoard
        (
            BoardEntity newBoard, 
            IEnumerable<BoardEntity> currentBoards, 
            IDictionary<string, int[]> newSpecialties,
            IDictionary<int, IEnumerable<BoardSpecialtyEntity>> currentSpecialties
        )
        {
            // TODO: following logic could go in separate BoardSpecialtyModification class.

            var currentBoard = currentBoards
                .Single(o => o.PublicId == newBoard.PublicId);

            newSpecialties.TryGetValue(newBoard.PublicId, out var newSpecialtyIds);

            var newSpecialtiesEntities = GetNewSpecialties(newSpecialtyIds, currentBoard.Id);

            currentSpecialties.TryGetValue(currentBoard.Id, out var currentSpecialtyEntities);

            await ModifyRelations(newSpecialtiesEntities, currentSpecialtyEntities, _boardSpecialtyComparer);

            await UpdateListMember(newBoard, currentBoards);
        }

        private async Task AddBoard(BoardEntity newBoard, IDictionary<string, int[]> publicIdToSpecialtiesMap)
        {
            publicIdToSpecialtiesMap.TryGetValue(newBoard.PublicId, out var newSpecialtyIds);

            var newBoardSpecialties = GetNewSpecialties(newSpecialtyIds, newBoard);

            _dbContextEntity.AddRange(newBoardSpecialties);
        }

        private async Task RemoveBoard(IEnumerable<BoardEntity> oldBoards, IDictionary<int, IEnumerable<BoardSpecialtyEntity>> privateIdToSpecialtiesMap) 
        {
            foreach (var board in oldBoards) {

                privateIdToSpecialtiesMap.TryGetValue(board.Id, out var oldSpecialties);
                _dbContextEntity.RemoveRange(oldSpecialties);
                _dbContextEntity.RemoveRange(board);
                
            }
        }

        private static IEnumerable<BoardSpecialtyEntity> GetNewSpecialties(int[] specialtyIds, int boardId)
        {
            return specialtyIds.Select(s => new BoardSpecialtyEntity
            {
                BoardId = boardId,
                SpecialtyId = s,
            });
        }

        private static IEnumerable<BoardSpecialtyEntity> GetNewSpecialties(int[] specialtyIds, BoardEntity newBoard)
        {
            return specialtyIds.Select(s => new BoardSpecialtyEntity
            {
                Board = newBoard,
                SpecialtyId = s,
            });
        }

        private async Task<IDictionary<int, IEnumerable<BoardSpecialtyEntity>>> GetPrivateIdToSpecialtiesMap(IEnumerable<BoardEntity> currentBoards)
        {
            var privateIdToSpecialtiesMap = new Dictionary<int, IEnumerable<BoardSpecialtyEntity>>();

            var currentBoardIds = currentBoards.Select(c => c.Id);

            var currentSpecialties = await _dbContextEntity.BoardSpecialties
                .Where(b => currentBoardIds.Contains(b.BoardId))
                .ToListAsync();

            foreach(var boardId in currentBoardIds)
            {
                var specialties = currentSpecialties.Where(s => s.BoardId == boardId);

                privateIdToSpecialtiesMap.Add(boardId, specialties);
            }

            return privateIdToSpecialtiesMap;
        }
    }
}
