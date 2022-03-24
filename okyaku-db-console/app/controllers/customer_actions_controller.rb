class CustomerActionsController < ApplicationController
  before_action :set_customer_action_and_transition, only: [:show, :edit, :update, :destroy]

  # GET /customer_actions
  # GET /customer_actions.json
  def index
    @customer_actions = CustomerAction.all
  end

  # GET /customer_actions/1
  # GET /customer_actions/1.json
  def show
  end

  # GET /customer_actions/new
  def new
    @customer_action = CustomerAction.new
  end

  # GET /customer_actions/1/edit
  def edit
  end

  # POST /customer_actions
  # POST /customer_actions.json
  def create
    @customer_action = CustomerAction.new(customer_action_params)

    respond_to do |format|
      if @customer_action.save
        format.html { redirect_to transition_customer_action_path(@transition), notice: 'Customer action was successfully created.' }
        format.json { render :show, status: :created, location: @customer_action }
      else
        format.html { render :new }
        format.json { render json: @customer_action.errors, status: :unprocessable_entity }
      end
    end
  end

  # PATCH/PUT /customer_actions/1
  # PATCH/PUT /customer_actions/1.json
  def update
    respond_to do |format|
      if @customer_action.update(customer_action_params)
        format.html { redirect_to transition_customer_action_path(@transition), notice: 'Customer action was successfully updated.' }
        format.json { render :show, status: :ok, location: @customer_action }
      else
        format.html { render :edit }
        format.json { render json: @customer_action.errors, status: :unprocessable_entity }
      end
    end
  end

  # DELETE /customer_actions/1
  # DELETE /customer_actions/1.json
  def destroy
    @customer_action.destroy
    respond_to do |format|
      format.html { redirect_to transition_path(@transition), notice: 'Customer action was successfully destroyed.' }
      format.json { head :no_content }
    end
  end

  private
    # Use callbacks to share common setup or constraints between actions.
    def set_customer_action_and_transition
      @customer_action = CustomerAction.find_by(transition_id: params[:transition_id])
      @transition = @customer_action.transition
    end

    # Never trust parameters from the scary internet, only allow the white list through.
    def customer_action_params
      params.require(:customer_action).permit(:name, :transition_id, :text_en, :text_ja)
    end
end
