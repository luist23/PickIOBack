insert into AspNetRoleClaims (Id, RoleId, ClaimType, ClaimValue)
values  (1, 'd0c75962-2743-4f31-9985-8237a5fe01a3', 'Permission', 'Permissions.Products.Read'),
        (2, 'd0c75962-2743-4f31-9985-8237a5fe01a3', 'Permission', 'Permissions.Products.Create'),
        (3, 'd0c75962-2743-4f31-9985-8237a5fe01a3', 'Permission', 'Permissions.Products.Edit'),
        (4, 'd0c75962-2743-4f31-9985-8237a5fe01a3', 'Permission', 'Permissions.Products.Delete'),
        (5, 'd0c75962-2743-4f31-9985-8237a5fe01a3', 'Permission', 'Permissions.Users.Read'),
        (6, 'd0c75962-2743-4f31-9985-8237a5fe01a3', 'Permission', 'Permissions.Users.Create'),
        (7, 'd0c75962-2743-4f31-9985-8237a5fe01a3', 'Permission', 'Permissions.Users.Edit'),
        (8, 'd0c75962-2743-4f31-9985-8237a5fe01a3', 'Permission', 'Permissions.Users.Delete'),
        (9, '1480c567-3230-40ae-ad8a-a502bd5f3371', 'Permission', 'Permissions.Products.Read'),
        (10, '1480c567-3230-40ae-ad8a-a502bd5f3371', 'Permission', 'Permissions.Products.Create'),
        (11, '1480c567-3230-40ae-ad8a-a502bd5f3371', 'Permission', 'Permissions.Products.Edit'),
        (12, '1480c567-3230-40ae-ad8a-a502bd5f3371', 'Permission', 'Permissions.Products.Delete'),
        (13, '1480c567-3230-40ae-ad8a-a502bd5f3371', 'Permission', 'Permissions.Users.Read'),
        (14, '1480c567-3230-40ae-ad8a-a502bd5f3371', 'Permission', 'Permissions.Users.Create'),
        (15, '1480c567-3230-40ae-ad8a-a502bd5f3371', 'Permission', 'Permissions.Users.Edit'),
        (16, '31dd2af6-265f-44b6-a2bc-7684a05e9f99', 'Permission', 'Permissions.Products.Read');

insert into AspNetRoles (Id, LevelAccess, Name, NormalizedName, ConcurrencyStamp)
values  ('d0c75962-2743-4f31-9985-8237a5fe01a3', 100, 'SuperAdmin', 'SUPERADMIN', 'a47d52d2-ee04-4e6d-8f94-57cff493c97b'),
        ('1480c567-3230-40ae-ad8a-a502bd5f3371', 50, 'Admin', 'ADMIN', 'eaae8d04-bd3d-45c4-bd57-86d320dcbca4'),
        ('31dd2af6-265f-44b6-a2bc-7684a05e9f99', 1, 'User', 'USER', 'c6fbbb1e-0e45-415f-8f62-c91b9c7a5012');

insert into AspNetUsers (Id, Name, LastName, Active, UserName, NormalizedUserName, Email, NormalizedEmail, EmailConfirmed, PasswordHash, SecurityStamp, ConcurrencyStamp, PhoneNumber, PhoneNumberConfirmed, TwoFactorEnabled, LockoutEnd, LockoutEnabled, AccessFailedCount)
values  ('96930e3c-6d19-4b1a-9e96-14d608a61d7d', 'ADMIN NAME', 'ADMIN LAST NAME', 1, 'admin', 'ADMIN', null, null, 0, 'AQAAAAIAAYagAAAAECzOdunW6RkGuxJzt2AJtVINJH6EdQ0p7RAW5mTi7FSFQAbY9Zi8poPnrO7Gv8ua6A==', '4YPO7EJ3PXQ2GPQ6TPQZQDWBQYSGSENB', 'c7d12460-82d8-4a4d-8070-e57155e05abf', null, 0, 0, null, 1, 0);

insert into AspNetUserRoles (UserId, RoleId)
values  ('96930e3c-6d19-4b1a-9e96-14d608a61d7d', 'd0c75962-2743-4f31-9985-8237a5fe01a3');


insert into BarCode (Code, InternalCode, UpdatedAt, CreatedAt, DeletedAt)
values  ('tetsdvg', 'agsdv', 20260415141122, 20260415141122, null),
        ('asd', 'asd', 20260622134723, 20260622134723, null),
        ('ns da', ' sn c', 20260622135822, 20260622135822, null);






