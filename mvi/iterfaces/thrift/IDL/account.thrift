namespace csharp mvi.services.interfaces.account

enum TPowerType {
  menu = 1,
  operate = 2
}
struct TPower
{
	1:i32 PowerId,
	2:string PowerName,
	3:TPowerType type,
}


struct TRole
{
	1:i32 RoleId,
	2:string RoleName,
	3:list<TPower> Powers,
}


struct TAccount
{
	1:i32 Id,
	2:string Name,
	3:string Password,//密码经过加密的哈希值
	4:string CreatedAt,
	5:string UpdatedAt,
	6:string Remark,
	7:bool IsEnabled,
	8:list<TRole> Roles,
}

struct TPageAccount
{
	1:i32 TotalCount,//查询记录总数
	2:i32 PageSize,//每页多少条数据
	3:i32 CurrPage,//当前第几页
	4:list<TAccount> Data,
}

struct TCommonReply
{
	1:bool IsSuccess,
	2:string Message,
}


service TAccountService
{
	TCommonReply SignIn(1:string name,2:string password)
	TCommonReply UpdatePassword(1:string name,2:string oldPassword,3:string newPassword)
	
	TAccount GetAccountByName(1:string name)
	TCommonReply AddAccount(1:TAccount account) 
	TCommonReply DelAccount(1:string name) 
	TCommonReply UpdateAccount(1:TAccount account)
	list<TAccount> GetAllAccounts()
	TPageAccount GetAccountsByPage(1:i32 pageIndex,2:i32 pageSize)

	TCommonReply AddRole(1:TRole role) 
	TCommonReply DelRole(1:i32 roleId) 
	TCommonReply UpdateRole(1:TRole role)
	list<TRole> GetAllRoles()

	TCommonReply AddPower(1:TPower power) 
	TCommonReply DelPower(1:i32 roleId) 
	list<TPower> GetAllPowers()
	
}