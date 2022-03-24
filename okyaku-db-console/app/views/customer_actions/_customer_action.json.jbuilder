json.extract! customer_action, :id, :name, :transition_id, :text_en, :text_ja, :created_at, :updated_at
json.url customer_action_url(customer_action, format: :json)
