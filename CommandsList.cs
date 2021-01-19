using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MasterArmsDiscordBot
{
	public class CommandsList : BaseCommandModule
	{
		[Command("AddRole")] // Command
		[RequireRoles(RoleCheckMode.All, "Admin")] // Require member to have role "Admin"
		public async Task AddRole(CommandContext ctx, string roleName) 
		{
			var commandMessage = ctx.Message; // Get command message
			var guild = ctx.Guild; // Get guild (server)
			var role = await guild.CreateRoleAsync(roleName); // Create role with name specified in command
			var confirmationMessage = await ctx.Channel.SendMessageAsync($"Created {role.Name}."); // Confirmation message
			await Task.Delay(4000); // Delay for 4 seconds
			await commandMessage.DeleteAsync(); // Delete the command message
			await confirmationMessage.DeleteAsync(); // Delete confirmation message
		}

		[Command("Role")] // Command
		public async Task Role(CommandContext ctx, DiscordRole RoleName)
		{
			var commandMessage = ctx.Message; // Get command message
			var caller = ctx.Member; // Get person who executed the command
			var roles = ctx.Guild.Roles.Values.ToList(); // Convert collection to list
			var roleCheck = roles.Contains(RoleName); // Check if roles list contains desired role
			var getRoleName = RoleName.Name; // Get name of the role

			if (roleCheck == true && getRoleName != "Admin" && getRoleName != "Moderator") // If role exists and desired role is not Admin.
			{
				if (caller.Roles.Contains(RoleName) == false) // If person does not have specified role
				{
						await caller.GrantRoleAsync(RoleName); // Grant desired role
						var grantConfirmation = await ctx.Channel.SendMessageAsync($"Assigned {RoleName.Name} to {ctx.Member.DisplayName}."); // Send a confirmation message
						await Task.Delay(4000); // Wait for 4 seconds
						await grantConfirmation.DeleteAsync(); // Delete the confirmation message
				}
				else if (caller.Roles.Contains(RoleName) == true) // If person has the specified role already
				{
					await caller.RevokeRoleAsync(RoleName); // Revoke specified role
					var revokeConfirmation = await ctx.Channel.SendMessageAsync($"Revoked {RoleName.Name} from {ctx.Member.DisplayName}."); // Send a confirmation message
					await Task.Delay(4000); // Wait for 4 seconds
					await revokeConfirmation.DeleteAsync(); // Delete confirmation message
				}
			}
			else if (roleCheck = true && (getRoleName == "Admin" || getRoleName == "Moderator")) // If person tried to give themselves Admin role
			{
				var warning = await ctx.Channel.SendMessageAsync("Haha, you funny man!"); // Fun message
				await Task.Delay(4000); // Wait for 4 seconds
				await warning.DeleteAsync(); // Delete fun message
			}
			else  // If neither of the above is true. Doesn't work for some reason I am not interested enough in finding out.
			{
				var nonexistent = await ctx.Channel.SendMessageAsync("Role does not exist."); // Send a message stating the specified role does not exist
				await Task.Delay(4000); // Wait for 4 seconds
				await nonexistent.DeleteAsync(); // Delete message
			}

			await commandMessage.DeleteAsync(); // Delete the command message
		}
	}
}
