﻿DECLARE @CurrentMigration [nvarchar](max)

IF object_id('[dbo].[__MigrationHistory]') IS NOT NULL
    SELECT @CurrentMigration =
        (SELECT TOP (1) 
        [Project1].[MigrationId] AS [MigrationId]
        FROM ( SELECT 
        [Extent1].[MigrationId] AS [MigrationId]
        FROM [dbo].[__MigrationHistory] AS [Extent1]
        WHERE [Extent1].[ContextKey] = N'WebApplication13.Migrations.Configuration'
        )  AS [Project1]
        ORDER BY [Project1].[MigrationId] DESC)

IF @CurrentMigration IS NULL
    SET @CurrentMigration = '0'

IF @CurrentMigration < '202004302323531_InitialSnapshot'
BEGIN
    CREATE TABLE [dbo].[categories] (
        [categoryid] [int] NOT NULL IDENTITY,
        [name] [nvarchar](max),
        CONSTRAINT [PK_dbo.categories] PRIMARY KEY ([categoryid])
    )
    CREATE TABLE [dbo].[products] (
        [id] [int] NOT NULL IDENTITY,
        [name] [nvarchar](max),
        [price] [real] NOT NULL,
        [Description] [nvarchar](max),
        [image] [nvarchar](max),
        [categoryid] [int] NOT NULL,
        CONSTRAINT [PK_dbo.products] PRIMARY KEY ([id])
    )
    CREATE INDEX [IX_categoryid] ON [dbo].[products]([categoryid])
    CREATE TABLE [dbo].[Userapps] (
        [userId] [int] NOT NULL IDENTITY,
        [name] [nvarchar](max) NOT NULL,
        [Email] [nvarchar](max) NOT NULL,
        [Password] [nvarchar](100) NOT NULL,
        [phone] [nvarchar](max) NOT NULL,
        [country] [nvarchar](max) NOT NULL,
        [image] [nvarchar](max) NOT NULL,
        [uR] [nvarchar](max),
        CONSTRAINT [PK_dbo.Userapps] PRIMARY KEY ([userId])
    )
    CREATE TABLE [dbo].[AspNetRoles] (
        [Id] [nvarchar](128) NOT NULL,
        [Name] [nvarchar](256) NOT NULL,
        CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY ([Id])
    )
    CREATE UNIQUE INDEX [RoleNameIndex] ON [dbo].[AspNetRoles]([Name])
    CREATE TABLE [dbo].[AspNetUserRoles] (
        [UserId] [nvarchar](128) NOT NULL,
        [RoleId] [nvarchar](128) NOT NULL,
        CONSTRAINT [PK_dbo.AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId])
    )
    CREATE INDEX [IX_UserId] ON [dbo].[AspNetUserRoles]([UserId])
    CREATE INDEX [IX_RoleId] ON [dbo].[AspNetUserRoles]([RoleId])
    CREATE TABLE [dbo].[AspNetUsers] (
        [Id] [nvarchar](128) NOT NULL,
        [Email] [nvarchar](256),
        [EmailConfirmed] [bit] NOT NULL,
        [PasswordHash] [nvarchar](max),
        [SecurityStamp] [nvarchar](max),
        [PhoneNumber] [nvarchar](max),
        [PhoneNumberConfirmed] [bit] NOT NULL,
        [TwoFactorEnabled] [bit] NOT NULL,
        [LockoutEndDateUtc] [datetime],
        [LockoutEnabled] [bit] NOT NULL,
        [AccessFailedCount] [int] NOT NULL,
        [UserName] [nvarchar](256) NOT NULL,
        CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY ([Id])
    )
    CREATE UNIQUE INDEX [UserNameIndex] ON [dbo].[AspNetUsers]([UserName])
    CREATE TABLE [dbo].[AspNetUserClaims] (
        [Id] [int] NOT NULL IDENTITY,
        [UserId] [nvarchar](128) NOT NULL,
        [ClaimType] [nvarchar](max),
        [ClaimValue] [nvarchar](max),
        CONSTRAINT [PK_dbo.AspNetUserClaims] PRIMARY KEY ([Id])
    )
    CREATE INDEX [IX_UserId] ON [dbo].[AspNetUserClaims]([UserId])
    CREATE TABLE [dbo].[AspNetUserLogins] (
        [LoginProvider] [nvarchar](128) NOT NULL,
        [ProviderKey] [nvarchar](128) NOT NULL,
        [UserId] [nvarchar](128) NOT NULL,
        CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey], [UserId])
    )
    CREATE INDEX [IX_UserId] ON [dbo].[AspNetUserLogins]([UserId])
    CREATE TABLE [dbo].[Userappproducts] (
        [Userapp_userId] [int] NOT NULL,
        [product_id] [int] NOT NULL,
        CONSTRAINT [PK_dbo.Userappproducts] PRIMARY KEY ([Userapp_userId], [product_id])
    )
    CREATE INDEX [IX_Userapp_userId] ON [dbo].[Userappproducts]([Userapp_userId])
    CREATE INDEX [IX_product_id] ON [dbo].[Userappproducts]([product_id])
    ALTER TABLE [dbo].[products] ADD CONSTRAINT [FK_dbo.products_dbo.categories_categoryid] FOREIGN KEY ([categoryid]) REFERENCES [dbo].[categories] ([categoryid]) ON DELETE CASCADE
    ALTER TABLE [dbo].[AspNetUserRoles] ADD CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[AspNetRoles] ([Id]) ON DELETE CASCADE
    ALTER TABLE [dbo].[AspNetUserRoles] ADD CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
    ALTER TABLE [dbo].[AspNetUserClaims] ADD CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
    ALTER TABLE [dbo].[AspNetUserLogins] ADD CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
    ALTER TABLE [dbo].[Userappproducts] ADD CONSTRAINT [FK_dbo.Userappproducts_dbo.Userapps_Userapp_userId] FOREIGN KEY ([Userapp_userId]) REFERENCES [dbo].[Userapps] ([userId]) ON DELETE CASCADE
    ALTER TABLE [dbo].[Userappproducts] ADD CONSTRAINT [FK_dbo.Userappproducts_dbo.products_product_id] FOREIGN KEY ([product_id]) REFERENCES [dbo].[products] ([id]) ON DELETE CASCADE
    CREATE TABLE [dbo].[__MigrationHistory] (
        [MigrationId] [nvarchar](150) NOT NULL,
        [ContextKey] [nvarchar](300) NOT NULL,
        [Model] [varbinary](max) NOT NULL,
        [ProductVersion] [nvarchar](32) NOT NULL,
        CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY ([MigrationId], [ContextKey])
    )
    INSERT [dbo].[__MigrationHistory]([MigrationId], [ContextKey], [Model], [ProductVersion])
    VALUES (N'202004302323531_InitialSnapshot', N'WebApplication13.Migrations.Configuration',  0x1F8B0800000000000400DD5D596FE4B8117E0F90FF20E82909BCDD3E328389D1BD0BAF3D4E8C8C0FB8ED4DDE0CB644B785D1B53A666D2CF697ED437E52FE42488994C44BA2EE76B0C0C22D921F8BC5AA62B148D6FCF7F7FFAC7E78F55CE31B8C6227F0D7E6D1E2D034A06F05B6E3EFD6669A3C7FF7C9FCE1FB3FFE61F5D9F65E8D9F68BD135C0FB5F4E3B5F99224E1E972195B2FD003F1C273AC288883E7646105DE12D8C1F2F8F0F06FCBA3A325441026C2328CD57DEA278E07B31FE8E779E05B304C52E05E07367463F21D956C3254E30678300E8105D7E6BFE0F62C0C5DC7020922E5E8649137318D33D701889C0D749F4D03F87E9064354E1F63B849A2C0DF6D42F401B80F6F2144F59E811B433288D3B2BAEE780E8FF1789665430A65A57112782D018F4E0883967CF34E6C360B0622167E46AC4EDEF0A83336AE4DC439B80BA237D3E03B3B3D77235C51C9E5056D7B60F0350E0AE9404284FF3B30CE53374923B8F6619A44C03D30EED22D6AF04FF8F6107C85FEDA4F5DB74A2A221695311FD0A7BB28086194BCDDC3676E008E6D1A4BB6FD9207289A4BDAE643BDF2939363D3B841C480AD0B0BC9A8B065930411FC3BF461845ADB772049608426F6CA86196F052AB83E7DF47FDA1B1245A45AA6710D5EBF407F97BCAC4DF4A7695C3AAFD0A65F08058FBE833411354AA2140A9DDC806FCE2EA38FEB2E8C023BB512A412F7D0CD2AC42F4E986BC682143E9542701905DE7DE0960D8BB2A70710ED6082480F141536411A591C69AB652971B57248D0BA8821693AAB147691BEF72F7582AC3956D90BEAC3859241D5635CC0D88A9C30B77623D3EB7860373E57F48D8CAE4E97EADA57A7A9CA2A759A2ABD2E69698C04340CE5E6E6312F7CBA2B6C52491A5F26981BA1422F7343D0BA981BD274567383D97CD5C1E4D076EFCCECB4B4219F3DE0B8D3777B07E2F89720B26B7A3E3A3C1CA3E7F025F067E0B31520CF1D5B98A93B1ECA70B7EC36BD9FD071BBAB73DC5A5952DEC82B4D6D274B4A4D01C6AE33A7D7C51EE52C0E6F60B2A00D1739E46584E090FA7C5D54110F0CED76A5F93DD635BF2747DBE7934F1F3E02FBE4E35FE1C9872EA6B88B19BEAAB511C79FC610DE9B7A537CFCE1E320BD2AE5194B9D5C98ABF3FD44AA95E22C960A022DA932884863A8E1C59AA2EEBF683F4ABD0C69553CA02E9AF0C83824D36903A577DC7E5BB9A35D7DD1591DD1F762FD9A5C425DF3A7D1CB79E03F3B91078B51FE182061037E677FF21F207E197D97BA81561A21A1DC24C00B47EFED0E3BAC37A9B7C5623F5D5F834DCDC32FC125B0D0DEE8B38F5BF5C6FB12585F8334F9ECDB17689FF598581410FF7C703C7D8041C839B32C18C7974898A17D8E9DFC96910B89919FDBFD387781E3A99DE9275ACEFAD1E4B3D485A6656D43245F829DE3D75042CB394AF2CF724A48595B4A30420D21A498A323FB2A27232F1ACCF5CAF83BBCEF95C1EEBFF3D56F651D3FB43397E7964D1FEE74F48523EBE927E0A64377D5491B321D1F5E1B32D8FDD7868C4CF4F99B636397416347422B2378ADFAF2CD4EB3CE71944DAD0ECC30A7EE7C1A1BA05297B3380E2C27D302F610B13CFB60A947EE95D17410928FA4729082C683E4DBC1DB2B44C3DAFC8BC0941AD8E210A3842DCF6558DC239397D15BFF02BA3081C69995DF1D3807B1056C7156107F6CF60B126B18610506783B1223B574FC44D401C7B79C10B80DD473ED5A9EC263FA8A9EF8920B18421F5B9A8679E94F42D113C7BA264EAD961531AB973E2128AB12137584B61493E2804A5FFAD447686D85BAC5A025C13B157D7591BC9242369E3CA28ED41226619D18156C9A9A4E5AA8E6928E1248C375FAFAA7E6844EE7F220E084DA477771751A226CE9589D9B48E4641B4885AC916DD028C22661C804522619BC4EAFCA70F464E245B6E6B593CAEFD3E7132F2E2AA0102FB2AF184FBC58864C255EECE0DF8778E50197DA39E5A22FF309171BEB997E9914B93195643123DF33C1CA374AA84D825AC0888673CBC3A18B2D2E84AFB28B96884E124C88C9BE8C97000CBE8109EB703B384858EED0846D8EE0AFB228E5055501A370561B2052EA2B0B088517DD8040429D427BD6196D00E125BF0EB0D48E0650721E2E1D592B8A6880BB9624E26BB480A5D1EA5A58B2C670B0151916C4A1727DB1524D75C591572C8D8D7E31A6AA040A1AAAB1B5AF00551582B7A7EC603518215EF1111951BFE3D4DB7356E84FE5DAA2B7C9D46068072EC8EE86887C68DA84EA6E432B63200A5CC3899A5D630547621406118DE2C04A2E15D29D50E35EA84237E1758324F05B18C5C029ADC38C9C9A1CC5C8654E7AA39BDE7EE49C77AD1839A57598911301520C5CE23E363990ED87CDFA7D03093A8DF0168E4B51B65AE68FBEC887D552F13A6C758D8C92E3EF2AAFC5C81763933F153BFF6ED3FEF99497632C2D86AFBC9B55F4940411D841AE14758D28BD74A238B90009D8021CDF3EB73DA19AD44D53ACC4B44BC11313A78FAECBB409FE3B6FD6F4AE4BE2DD12904B344E0FFBC5D921A2B8044A9A1AF8FD1E704154F33CE23C7053CF6F0EE7AAD1F20BE7559CFC8B88B05A726311BC738175C216899D0CADA90AD54B75FB89A20E72FB7952B65431969F9EB1A6458540DE165521C8277D0CE66D51158929D0C723D7D019B6E49FF431BA89FF4CC24B1DC42164F7518EA521BBCA962A1ED3572F55FECA5FD0D4A1F497617209B10A413EE963948F4CAA30E5D7161A953F1A61342AFFD4427AE90B104674E9C7693509BFCE6066F87E8F3447E59BB5D71B2624D15E79EA9BAB78CB6B4F3BCDB91134E766BF1665D1771D72A68A584FF7D95243A878FE28B179AA40A41A859EE8555154A77CB3CD9E6AC3DF6D4DEAB8204DA54B43AC21FC357501AC52D67E65CAAFABCB56A7BC441F91BB935E85E48A5A5059BD79CE10592DE884A7E0A8BC867E0FE25DF32ABA58AA8F2CB9755E85961477C096D0CC97E9A34A2EA6578125C5FAD8E52D75DE66EEF16AA50CF3F55BAEF273807EEB9502631CB338CC7257B95B5C05AA7C6E89456E0F0B60E4FB5E8A94327EDA4FA4F233A07E22A5C050DB20E66A2E6B826AEF13AB3199FBB68C99AFBB6FACC66B27B8638B071BA115DD2CF6404A7B935F69A3D8CC2BE25138FA2C6198EC204BE495762C8042AA6202D96D088AD38142E55D01AD481B3D7994DF6B5551C647DBC5E91682EE7C9542D88AE03B17645F918077739E3621029E57310DAA35C8A37B8B13E82D7085C5E667F7DC75205EBF69856BE03BCF304EF22705E6F1E1D13197E56D7F32AE2DE3D876250706B2B46BECA4CD90F1CCC15C6E7C0BD4F261401E19CB3BF0BF81C87A01D19F3CF0FAE72A528F873152733145A6AE7DE29608C424E08A20708748BFD58B2226534B2F2485E40AB798AE7C1BBEAECD5FB366A7C6D5BF9FCA9607C66D846CC9A97168FC36E4D37D211A3D6516A7F944B2FDCBFB7E507C82258A26A658EA933FA91F8D5C4EA47E607ADAD33277D118F6581D559EE2B1AB5C0EF0D3B31E39724AD0261383078DFFCA3E1F1857F1A3EFFC9CA28207C4506C68F877EEC3181E8D2C3505A1BFBD8BF42FFA2C47563D6FCA5AF49ED3CF268569454DDEB40735ED52C5BC5F15932E07752AD23DEFCAD64906C9B9D2CB6D91E655E98528C99D3214DE202C54E546E982A5CC8B62A39F499617A5DD60E57952BA90A6CC9192F9633D33A4E8DB1EDA72C6C54712E89DC2248DE2F7EED78224E4BAE8A5E8623E8B315DC1BA9C15FDFC9277960B62B0A5F34E4CF53018F69C72DF76ABDD23F8D318E9159BA8C3AF7AEE6DB5BB76710BB6758BD8852C30540EA21D1165CB0182273D327694EF81A6CEA521BC01EA9625A4D32BC79A9BD86DE2C246871C1CEF33E786F434487CBEA4936A63C4D7B315232621645C9152DFBBD50D50B692A6FAF3BD6E467A16C1921DE275B30DFFA782D5668A7B9AA80E42557F8A3A8140754C94B32FB971CA28E3BC2971A6CC825373715737043AA1B5D2497E3357CA08C91BF399B2DD4C2D3FAA9B742DC2EDFB94DE661F0488441566CA6733B500A9EECDEDA3003527B0D907F9996B199B437AB497AF098587BB7646A393C223697E2ED92424E5DEBF8C35086181FC9AD9DAB4B7019AF13C5E5193A583EFA37098852E8A12590FA12AFF058F5FEC3C05FCA244864F0A9BF1597F51E8842D96F5942703963FE75775566A91B2C3B28ABA53751E011917951972EA3B68372AE29BD40E8BD4A9EF56910FA3AE6FB2ACD5F64DEAD4F7ADC848A1104DB506F0156A0455A90F536400E2ED049BA640B477922D7DA561F18D5F7B7AE7F729727234A7F9515C799685B9C49BD6B54396CFBAECA2F6080CA04945341820BF51DD6DEE6663C0C8B98DA4D9596439C71A1C25C55B0C69F6B21973190973A53D4CC6AC2B9E310D34D0215217751F28B386281ED70C34D0FE998ABA0F7348B16D9199487C0C817CE4CA3F6D8FFCF4D8D99510F81FBAF7A1C578C7459D2BFF39A04E3A4711ADC21DF15DC304D8C8753E8B12E71958092AC61754B28CFCD9A13FBE26B585F6957F9B26619AA021436FEB324B2076F6EBFACFD22FB134AF6EB3CBE2F1104340643AF862CFADFF63EAB87641F7A5E4845101817711E43A089ECB045F0BD9BD154837427E0B1510615FB1F979805EE822B0F8D6DF806FB00B6D48FCBEC01DB0DECA1B022A90E68960D9BEBA70C02E025E4C30CAF6E8279261DB7BFDFE7F5FECF9A1E1810000 , N'6.2.0-61023')
END

