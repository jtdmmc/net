namespace csharp MVI.DotnetSoftwarePlatform.Api.Thrift

//端口号
const i32 SERVER_PORT = 7001 

// windows file time 
// https://msdn.microsoft.com/en-us/library/windows/desktop/ms724290(v=vs.85).aspx
typedef i64 TDateTime 

struct TPower
{
	1:i32 PowerId,
	2:string PowerName,
}


struct TRole
{
	1:i32 RoleId,
	2:string RoleName,
	3:list<TPower> Powers,
}


struct TAccount
{
	1:string UniqueName,
	2:string Password,//密码经过加密的哈希值
	3:TDateTime CreatedAt,
	4:TDateTime UpdatedAt,
	5:string Remark,
	6:bool IsEnabled,
	7:list<TRole> Roles,
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

exception TNotFoundException
{
	1:string Reason,
}

exception TUnknownErrorException
{
	1:string Reason,
}

exception TAlreadyExistException
{
	1:string Reason,
}

service TAccountService
{
	TCommonReply SignInWithAccount(1:string uniqueName,2:string password)throws  (1:TNotFoundException nfe, 2:TUnknownErrorException ex);
	TCommonReply UpdatePassword(1:string uniqueName,2:string oldPassword,3:string newPassword)throws (1:TNotFoundException nfe, 2:TUnknownErrorException ex);
	
	TAccount GetAccountByUniqueName(1:string uniqueName)throws (1:TNotFoundException nfe, 2:TUnknownErrorException ex);
	TCommonReply AddAccount(1:TAccount account) throws (1:TUnknownErrorException ex);
	TCommonReply DelAccount(1:string uniqueName) throws (1:TNotFoundException nfe, 2:TUnknownErrorException ex);
	TCommonReply UpdateAccount(1:TAccount account)throws (1:TNotFoundException nfe, 2:TUnknownErrorException ex);
	list<TAccount> GetAllAccounts()throws  (1:TNotFoundException nfe, 2:TUnknownErrorException ex);
	TPageAccount GetAccountsByPage(1:i32 pageIndex,2:i32 pageSize)throws  (1:TNotFoundException nfe, 2:TUnknownErrorException ex); 

	TCommonReply AddRole(1:TRole role) throws (1:TUnknownErrorException ex);
	TCommonReply DelRole(1:i32 roleId) throws  (1:TNotFoundException nfe, 2:TUnknownErrorException ex);
	TCommonReply UpdateRole(1:TRole role)throws (1:TNotFoundException nfe, 2:TUnknownErrorException ex);
	list<TRole> GetAllRoles()throws  (1:TNotFoundException nfe, 2:TUnknownErrorException ex);

	//TCommonReply AddPower(1:TPower power) throws (1:TUnknownErrorException ex);
	//TCommonReply DelPower(1:i32 roleId) throws  (1:TNotFoundException nfe, 2:TUnknownErrorException ex);
	//TCommonReply UpdateRole(1:TPower roleId)throws (1:TNotFoundException nfe, 2:TUnknownErrorException ex);
	list<TPower> GetAllPowers()throws  (1:TNotFoundException nfe, 2:TUnknownErrorException ex);
	
}