using ChatJaffApp.Server.ChatRoom.Encryption;
using ChatJaffApp.Server.Data.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatJaffApi.Tests.UnitTests
{
    public class ChatKeyRepositoryTests
    {
        public ChatKeyRepositoryTests()
        {
            IList<ChatKey> keys = new List<ChatKey>
            {
                new ChatKey
                {
                    ChatRoomId = Guid.Parse("{5D728EC3-1F6B-4170-8827-BC064AE25A41}"),
                    Key = "123.banana"
                },
                new ChatKey
                {
                    ChatRoomId = Guid.Parse("b8381d75-d110-42f9-85e5-9c92a062fbc8"),
                    Key = "234.orange"
                },
            };

            Mock<IChatKeyRepository> mockChatKeyRepo = new Mock<IChatKeyRepository>();

            mockChatKeyRepo.Setup(mr => mr.GetChatKeyAsync(
                It.IsAny<Guid>())).Returns( async (Guid id) =>
                {
                    var key = keys.Where(k => k.ChatRoomId == id).FirstOrDefault();
                    if (key == null)
                    {
                        return string.Empty;
                    } else
                    {
                        return key.Key;
                    }
                });

            mockChatKeyRepo.Setup(mr => mr.DeleteChatKey(
                It.IsAny<Guid>())).Callback((Guid id) =>
                {
                    var keyToRemove = keys.First(k => k.ChatRoomId == id);
                    keys.Remove(keyToRemove);
                });

            this.MockChatKeyRepo = mockChatKeyRepo.Object;
        }

        public readonly IChatKeyRepository MockChatKeyRepo;

        [Fact]
        public async Task DeleteChatKey_OnSuccess_RemovesKeyFromDb()
        {
            var keyId = Guid.Parse("{5D728EC3-1F6B-4170-8827-BC064AE25A41}");
            var keyToRemove = await MockChatKeyRepo.GetChatKeyAsync(keyId);

            Assert.NotEqual(string.Empty, keyToRemove);

            await MockChatKeyRepo.DeleteChatKey(keyId);

            var nonExistantKey = await MockChatKeyRepo.GetChatKeyAsync(
                Guid.Parse("{5D728EC3-1F6B-4170-8827-BC064AE25A41}"));

            Assert.Equal(string.Empty, nonExistantKey);
        }
    }
}
