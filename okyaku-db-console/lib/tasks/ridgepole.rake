# frozen_string_literal: true

namespace :ridgepole do
  def tables_option
    tables = ENV.fetch('TABLES', '').split(',')
    tables.empty? ? [] : ['--tables', tables.join(',')]
  end

  desc 'Export schema definitions'
  task :export do
    sh 'ridgepole', '--config', 'config/database.yml', '--split', '--export', '--output', 'db/schema/Schemafile'
  end

  desc 'Show difference between schema definitions and actual schema'
  task :'dry-run' do
    sh 'ridgepole', '--config', 'config/database.yml', '--apply', '--dry-run', '--file', 'db/schema/Schemafile', *tables_option
  end

  desc 'Apply schema definitions'
  task :apply do
    sh 'ridgepole', '--config', 'config/database.yml', '--apply', '--file', 'db/schema/Schemafile', *tables_option
  end
end
