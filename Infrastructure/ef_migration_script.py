import subprocess

def get_user_input():
    print("Select the operation you want to perform:")
    print("1 - Update database")
    print("2 - Add migration")
    
    operation = input("Enter the number of the operation (1 or 2): ").strip()
    if operation not in ['1', '2']:
        print("Invalid input. Please enter 1 or 2.")
        return None, None
    
    connection_string = input("Enter the connection string: ").strip()
    
    if not connection_string:
        print("Connection string cannot be empty.")
        return None, None
    
    return operation, connection_string

def run_command(operation, connection_string):
    connection_string_escaped = connection_string.replace("'", "''")  # Escape single quotes in the connection string
    
    if operation == '1':
        command = f"dotnet ef database update -- --connection=\"{connection_string_escaped}\""
    elif operation == '2':
        migration_name = input("Enter the migration name: ").strip()
        if not migration_name:
            print("Migration name cannot be empty.")
            return
        command = f"dotnet ef migrations add {migration_name} -- --connection=\"{connection_string_escaped}\""
    
    try:
        print(f"Running command: {command}")
        subprocess.run(command, check=True, shell=True)
        print("Command executed successfully.")
    except subprocess.CalledProcessError as e:
        print(f"An error occurred while executing the command: {e}")

def main():
    operation, connection_string = get_user_input()
    if operation and connection_string:
        run_command(operation, connection_string)

if __name__ == "__main__":
    main()
